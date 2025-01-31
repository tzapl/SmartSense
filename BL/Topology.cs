namespace SmartSense.API.ServiceModule.BL
{
    /// <summary>
    /// Working with topology logic
    /// </summary>
    public class Topology
    {
        private TopologyData topologyData;

        /// <summary>
        /// Creating new topology
        /// </summary>
        /// <param name="topologyData">Topology with hubs and peripheries</param>
        public Topology(TopologyData topologyData)
        {
            this.topologyData = topologyData;
        }

        /// <summary>
        /// Returns states of pairing db peripheries and topology peripheries
        /// </summary>
        /// <param name="peripheries">Db peripheries</param>
        /// <param name="allPaired">If all thermo were paired</param>
        /// <returns>States of pairing</returns>
        public List<IODPeriphery> CheckPeripheries(List<Periphery> peripheries, out bool allPaired)
        {
            //our returned list of peripheries
            var pairedPeripheries = new List<IODPeriphery>();

            //known peripheries from topology
            var topologyPeripheries = topologyData.Peripheries.ToList();

            //modify collection according to unknownThermo
            var _peripheries = PairUnknownThermometer(peripheries, topologyPeripheries, out allPaired);

            #region Combine existing SerialNumbers
            var dbPeripheries = _peripheries.ToList();
            //check if exists peripheries in topology with same SN and type
            foreach (var p in dbPeripheries)
            {
                if (!string.IsNullOrEmpty(p.SerialNumber))
                {
                    //find pair periphery in topology
                    var tp = topologyPeripheries.Where(o => o.SerialNumber == p.SerialNumber && o.PeripheryType == p.Type).FirstOrDefault();
                    if (tp != null)
                    {
                        //we use this periphery - remove it
                        _peripheries.Remove(p);
                        topologyPeripheries.Remove(tp);

                        //add to paired
                        pairedPeripheries.Add(new IODPeriphery()
                        {
                            Id = p.Id,
                            SerialNumber = p.SerialNumber,
                            State = IODPeripheryState.OK,
                            Type = p.Type,
                            Slot = tp.Slot,
                            ParentHubId = tp.ParentHubId,
                            RootSlot = null
                        });
                    }
                }
            }

            // all paired now removed
            #endregion

            //group peripheries by their type
            var topologyPeripheriesByType = topologyPeripheries.GroupBy(o => o.PeripheryType).ToDictionary(o => o.Key, o => o.ToList());

            //go through all peripheries in db (except paired higher)
            foreach (var periphery in _peripheries)
            {
                //paired topology periphery
                TopologyItem? tpPaired = SearchForTopologyPeriphery(periphery, topologyPeripheriesByType);

                //if we found paired periphery in topology -> OK else notActive
                pairedPeripheries.Add(new IODPeriphery()
                {
                    Type = periphery.Type,
                    SerialNumber = tpPaired != null ? tpPaired.SerialNumber : periphery.SerialNumber,
                    Id = periphery.Id,
                    State = tpPaired != null ? IODPeripheryState.OK : IODPeripheryState.NotActive,
                    Slot = tpPaired != null ? tpPaired.Slot : null,
                    ParentHubId = tpPaired != null ? tpPaired.ParentHubId : null,
                    RootSlot = null
                });

                //remove paired item from active topologyPeripheries
                if (tpPaired != null)
                    topologyPeripheries.Remove(tpPaired);
            }

            //in topologyPeripheries we have topology peripheries which are not in db
            foreach (var periphery in topologyPeripheries)
            {
                pairedPeripheries.Add(new IODPeriphery()
                {
                    Type = periphery.PeripheryType,
                    SerialNumber = periphery.SerialNumber,
                    Id = 0,
                    State = IODPeripheryState.NotFunctional,
                    Slot = periphery.Slot,
                    ParentHubId = periphery.ParentHubId,
                    RootSlot = null
                });
            }

            //complete root slot
            int hubsCount = topologyData.Hubs.Count;
            foreach (var p in pairedPeripheries)
            {
                if (p.ParentHubId != null && p.ParentHubId < hubsCount)
                    p.RootSlot = topologyData.Hubs[p.ParentHubId.Value].Slot;
            }

            return pairedPeripheries;
        }

        /// <summary>
        /// Checks if paired peripheries have data
        /// </summary>
        public List<IODPeriphery> CheckData(IPeripheryRepository peripheryRepository, List<IODPeriphery> peripheries,
            string deviceId, DateTime from)
        {
            //device used peripheries from date
            var usedPeripheries = new HashSet<int>(peripheryRepository.GetPeripheriesIdWithDataFromDate(deviceId, from));

            //process all peripheries
            foreach (var p in peripheries)
            {
                //only ok peripheries
                if (p.State == IODPeripheryState.OK)
                {
                    //no data recieved from periphery
                    if (!usedPeripheries.Contains(p.Id))
                        p.State = IODPeripheryState.NotFunctional;
                }
            }

            //return all peripheries
            return peripheries;
        }

        /// <summary>
        /// Set topology to unit
        /// Method assumes that peripheries are valid against topology (at least CheckPeripheries returns valid response)
        /// </summary>
        public bool SetUnitByTopology(IPeripheryRepository peripheryRepository, IHubRepository hubRepository,
            TopologyData topologyData, Unit unit)
        {
            //unit id
            int unitId = unit.Id;

            //remove all old hubs
            var hubs = hubRepository.GetHubsByUnitId(unitId);
            foreach (var h in hubs)
                hubRepository.Delete(h);

            //create new hubs from topology
            foreach (var h in topologyData.Hubs)
                hubRepository.Add(new Hub()
                {
                    Id = 0,
                    ParentId = h.ParentHubId,
                    SerialNumber = h.SerialNumber,
                    Slot = h.Slot,
                    UnitId = unitId
                });

            //we created hubs now we can create peripheries from topology
            var dbPeripheries = peripheryRepository.GetUnitPeripheries(unitId)?.ToList() ?? new List<Periphery>();
            var topologyPeripheries = topologyData.Peripheries;

            //group peripheries by their type
            var topologyPeripheriesByType = topologyPeripheries.GroupBy(o => o.PeripheryType).ToDictionary(o => o.Key, o => o.ToList());

            //peripheries we need to change
            var changedPeripheries = new List<Periphery>();

            //foreach db periphery
            foreach (var p in dbPeripheries)
            {
                //search for topology periphery
                var tpPeriphery = SearchForTopologyPeriphery(p, topologyPeripheriesByType);

                //we found tp periphery
                if (tpPeriphery != null)
                {
                    //update periphery according to topology
                    p.SerialNumber = tpPeriphery.SerialNumber;
                    p.HubInternalId = tpPeriphery.ParentHubId;
                    p.Slot = tpPeriphery.Slot;

                    //add to list periphery for change
                    changedPeripheries.Add(p);

                    //remove from active topology peripheries
                    topologyPeripheries.Remove(tpPeriphery);
                }
            }

            //we need to save to db changed peripheries
            foreach (var p in changedPeripheries)
                peripheryRepository.UpdateTopology(p);

            //all is done
            return true;
        }

        /// <summary>
        /// Save thermometers type to db from topology
        /// </summary>
        /// <param name="peripheryRepository">PeripheryRepository</param>
        /// <param name="unit">Unit</param>
        public void SavePairedThermometers(IPeripheryRepository peripheryRepository, Unit unit)
        {
            var topologyPeripheries = topologyData.Peripheries.ToList();
            var dbPeripheries = peripheryRepository.GetUnitPeripheries(unit.Id)?.ToList() ?? new List<Periphery>();
            int topologyIndex = 0, topologyCount = topologyPeripheries.Count;
            //lets go through all peripheries
            foreach (var dbPeriphery in dbPeripheries)
            {
                //we have unknown thermo
                if (dbPeriphery.Type == (byte)PeripheryType.ThermometerUnknown)
                {
                    //can we find any image in topology?
                    byte? peripheryType = null;
                    for (int i = topologyIndex; i < topologyCount; i++)
                    {
                        topologyIndex = i + 1;
                        if (specialThermo.Contains(topologyPeripheries[i].PeripheryType))
                        {
                            peripheryType = topologyPeripheries[i].PeripheryType;
                            break;
                        }
                    }

                    //we've found periphery, lets change type
                    if (peripheryType != null)
                        peripheryRepository.UpdateType(dbPeriphery, peripheryType.Value);
                }
            }
        }

        private static HashSet<byte> specialThermo = new HashSet<byte>() { (byte)PeripheryType.ThermometerCoolingBath, (byte)PeripheryType.ThermometerCompressorInput, (byte)PeripheryType.ThermometerCompressorOutput, (byte)PeripheryType.ThermometerMotor };
        private static List<Periphery> PairUnknownThermometer(List<Periphery> dbPeripheries, List<TopologyItem> topologyPeripheries, out bool allPaired)
        {
            //peripheries with set proper thermometers
            var modifiedPeripheries = new List<Periphery>();

            allPaired = true;
            int topologyIndex = 0, topologyCount = topologyPeripheries.Count;
            //lets go through all peripheries
            foreach (var dbPeriphery in dbPeripheries)
            {
                var nPeriphery = new Periphery(dbPeriphery);

                //we have unknown thermo
                if (dbPeriphery.Type == (byte)PeripheryType.ThermometerUnknown)
                {
                    allPaired = false;
                    //can we find any image in topology?
                    byte? peripheryType = null;
                    for (int i = topologyIndex; i < topologyCount; i++)
                    {
                        topologyIndex = i + 1;
                        if (specialThermo.Contains(topologyPeripheries[i].PeripheryType))
                        {
                            allPaired = true;
                            peripheryType = topologyPeripheries[i].PeripheryType;
                            break;
                        }
                    }

                    //we've found periphery, lets change type
                    if (peripheryType != null)
                        nPeriphery.Type = peripheryType.Value;
                }

                modifiedPeripheries.Add(nPeriphery);
            }

            return modifiedPeripheries;
        }

        private TopologyItem? SearchForTopologyPeriphery(Periphery periphery, Dictionary<byte, List<TopologyItem>> topologyPeripheriesByType)
        {
            //paired topology periphery
            TopologyItem? tpPaired = null;

            //we need to find paired periphery from topology
            var peripheryType = periphery.Type;
            if (topologyPeripheriesByType.ContainsKey(peripheryType) &&
                topologyPeripheriesByType[peripheryType].Count > 0)
            {
                //we have found same periphery type in topology
                tpPaired = topologyPeripheriesByType[peripheryType][0];

                //we used first periphery from topology we need to remove it
                topologyPeripheriesByType[peripheryType].RemoveAt(0);
            }

            return tpPaired;
        }
    }
}