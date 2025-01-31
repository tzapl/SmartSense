using Microsoft.EntityFrameworkCore.Metadata.Conventions;
using Microsoft.EntityFrameworkCore.Migrations;
using Newtonsoft.Json.Linq;
using SmartSense.API.Base.Data;
using SmartSense.API.Base.Helpers;
using System.Net;
using System.Reflection.Metadata;
using System.Text;
using System.Text.Json;
using Resx = SmartSense.API.Base.Resources;


namespace SmartSense.API.ServiceModule.Services
{
    public interface IUnitService
    {
        Response<UnitDetail?> GetUnit(IConfiguration configuration, IMessageLogger logger, IUnitRepository unitRepository, IPeripheryRepository peripheryRepository, string setCode, string sessionId);
        Response<UnitDetail?> SetUnit(IConfiguration configuration, IMessageLogger logger, IUnitRepository unitRepository, IPeripheryRepository peripheryRepository, IPubRepository pubRepository, IHistoryActionRepository historyActionHistory, IHistoryActionEditRepository historyActionEditRepository, string handlePub, string deviceId, string setCode, string nfc, bool hasSimcard, List<PeripheryAddItem> peripheries, string sessionId);
        Response<string> StartMontage(IConfiguration configuration, IMessageLogger logger, DbContext context, IUnitRepository unitRepository, IServiceRepository serviceRepository, IUserPermissionRepository userPermissionRepository, ICommunicationIODAPIerrorRepository communicationIODAPIerrorRepository, string deviceId, string raAddress, string taskHandle, string userToken, string sessionId);
        Response<string> SuccessMontage(IConfiguration configuration, IMessageLogger logger, DbContext context, IUnitRepository unitRepository, IServiceRepository serviceRepository, IDeviceSettingsChangeRepository deviceSettingsChangeRepository, IUserPermissionRepository userPermissionRepository, ICommunicationIODAPIerrorRepository communicationIODAPIerrorRepository, string deviceId, string data, int interval, string sessionId, string raAddress, string taskHandle, string userToken);
        Response<string> CancelMontage(IConfiguration configuration, IMessageLogger logger, DbContext context, IUnitRepository unitRepository, IServiceRepository serviceRepository, IDeviceSettingsChangeRepository deviceSettingsChangeRepository, IUserPermissionRepository userPermissionRepository, string deviceId, string data, int interval, string sessionId);
        Response<string> SetUnitLowInterval(IConfiguration configuration, IMessageLogger logger, IUnitRepository unitRepository, IDeviceSettingsChangeRepository deviceSettingsChangeRepository, IUserPermissionRepository userPermissionRepository, string deviceId, string data, int interval, string sessionId);
        Response<string> SetSettings(IConfiguration configuration, IMessageLogger logger, IUnitRepository unitRepository, IDeviceSettingsChangeRepository deviceSettingsChangeRepository, IUserPermissionRepository userPermissionRepository, string deviceId, string data, bool? wifi, string? SSID, string? password, string sessionId);
        Response<PeripheryLocalizationInfo?> GetPeripheryLocalization(IConfiguration configuration, IMessageLogger logger, IPeripheryLocalizationRepository peripheryLocalizationRepository, int peripheryId, string sessionId);
        Response<string> SetTapUnitOrder(IConfiguration configuration, IMessageLogger logger, ITapUnitOrderRepository tapUnitOrderRepository, IUserPermissionRepository userPermissionRepository, IPeripheryRepository peripheryRepository, int peripheryId, int unitId, int order, string sessionId);
        Response<Dictionary<int, decimal>?> GetUnitPeripheriesData(IConfiguration configuration, IMessageLogger logger, IUnitPeripheriesDataFluidRepository unitPeripheriesDataFluidRepository, int unitId, DateTime from, string sessionId);
        Response<int> StartService(IConfiguration configuration, IMessageLogger logger, DbContext context, IUnitRepository unitRepository, IServiceRepository serviceRepository, IUserPermissionRepository userPermissionRepository, string deviceId, string sessionId);
        Response<string> SuccessService(IConfiguration configuration, IMessageLogger logger, DbContext context, IUnitRepository unitRepository, IServiceRepository serviceRepository, IUserPermissionRepository userPermissionRepository, ICommunicationIODAPIerrorRepository communicationIODAPIerrorRepository, string deviceId, string sessionId, string raAddress, string taskHandle, string userToken);
        Response<string> CancelService(IConfiguration configuration, IMessageLogger logger, DbContext context, IUnitRepository unitRepository, IServiceRepository serviceRepository, IUserPermissionRepository userPermissionRepository, string deviceId, string sessionId);
        Response<string> SetServiceNote(IConfiguration configuration, IMessageLogger logger, IUnitRepository unitRepository, IServiceRepository serviceRepository, IUserPermissionRepository userPermissionRepository, string deviceId, string note, string sessionId);
        Response<string> AddPicture(IConfiguration configuration, IMessageLogger logger, IUserPermissionRepository userPermissionRepository, IUnitServicePictureRepository unitServicePictureRepository, int serviceId, byte component, byte pictureType, byte[]? filedata, string filename, string sessionId);
        public Response<UnitDetail?> GetUnitByNFC(IConfiguration configuration, IMessageLogger logger, IUnitRepository unitRepository, IPeripheryRepository peripheryRepository, string NFC, string sessionId);
        Response<List<string>?> LogIOD(IConfiguration configuration, IMessageLogger logger, IUserPermissionRepository userPermissionRepository, IUnitRepository unitRepository, IUnitEventRepository unitEventRepository, string deviceId, DateTime from, string sessionId);
        Response<CheckIODResponse?> CheckIOD(IConfiguration configuration, IMessageLogger logger, IUserPermissionRepository userPermissionRepository, IUnitRepository unitRepository, IPeripheryRepository peripheryRepository, IHubRepository hubRepository, string deviceId, DateTime from, string sessionId);
        Response<ConfirmIODResponse?> ConfirmIOD(IConfiguration configuration, IMessageLogger logger, IUserPermissionRepository userPermissionRepository, IUnitRepository unitRepository, IPeripheryRepository peripheryRepository, IHubRepository hubRepository, string deviceId, DateTime from, string sessionId);
        Response<string> AddServiceComponents(IConfiguration configuration, IMessageLogger logger, IServiceRepository serviceRepository, IUserPermissionRepository userPermissionRepository, IServiceComponentRepository serviceComponentRepository, ITapUnitOrderRepository tapUnitOrderRepository, IUnitRepository unitRepository, IPubApiInfoRepository pubApiInfoRepository, IComponentPriceRepository componentPriceRepository, int unitId, List<byte> components, string sessionId);
        Response<AlertsList> GetAlertMessages(IConfiguration configuration, IMessageLogger logger, IUserPermissionRepository userPermissionRepository, IPeripheryRepository peripheryRepository, IAlertMessageInfoRepository alertMessageInfoRepository, IUnitRepository unitRepository, int unitId, string sessionId);
        Response<AlertsList> GetAllAlertMessages(IConfiguration configuration, IMessageLogger logger, IUserPermissionRepository userPermissionRepository, IPeripheryRepository peripheryRepository, IAlertMessageInfoRepository alertMessageInfoRepository, IUnitRepository unitRepository, int unitId, string sessionId);
        Response<string> DisassemblyUnit(IConfiguration configuration, IMessageLogger logger, DbContext context, IUserPermissionRepository userPermissionRepository, IUnitRepository unitRepository, IServiceRepository serviceRepository, IPubRepository pubRepository, IHistoryActionRepository historyActionRepository, IHistoryActionEditRepository historyActionEditRepository, int unitId, string sessionId);
        Response<string> ChangePeriphery(IConfiguration configuration, IMessageLogger logger, DbContext context, IUserPermissionRepository userPermissionRepository, IPeripheryRepository peripheryRepository, int peripheryId, string serialNumber, string sessionId);
        Response<WifiListResponse?> GetWifiList(IConfiguration configuration, IMessageLogger logger, IUnitRepository unitRepository, IUnitWifiRepository unitWifiRepository, string deviceId, string sessionId);
        Response<UnitDetail?> RemovePeripheries(IConfiguration configuration, IMessageLogger logger, IUnitRepository unitRepository, IPubRepository pubRepository, IPeripheryRepository peripheryRepository, IHistoryActionRepository historyActionRepository, IHistoryActionEditRepository historyActionEditRepository, string handlePub, string deviceId, List<PeripheryAddItem>? peripheriesToRemove, string sessionId);
        Response<UnitDetail?> ServicePeripheries(IConfiguration configuration, IMessageLogger logger, IUnitRepository unitRepository, IPubRepository pubRepository, IPeripheryRepository peripheryRepository, IHistoryActionRepository historyActionRepository, IHistoryActionEditRepository historyActionEditRepository, string handlePub, string deviceId, List<byte>? peripheries, string sessionId);
    }
    public class UnitService : IUnitService
    {
        public Response<UnitDetail?> GetUnit(IConfiguration configuration, IMessageLogger logger, IUnitRepository unitRepository, IPeripheryRepository peripheryRepository, string setCode, string sessionId)
        {
            var status = ResponseStatus.ErrorUnknown;
            UnitDetail? unit = null;
            try
            {

                var sessionManager = new SessionManager();

                if (sessionManager.Exists(sessionId, out User? user) &&
                    user != null)
                {
                    var unitDB = unitRepository.GetBySetCode(setCode);

                    if (unitDB != null)
                    {
                        unit = GetUnitDetail(peripheryRepository, unitDB);

                        status = ResponseStatus.OK;
                    }
                    else
                    {
                        status = ResponseStatus.ErrorUnitNotFound;
                    }

                }
                else
                    status = ResponseStatus.ErrorUserNotLogged;
            }
            catch (Exception ex)
            {
                logger.Log(configuration, ex);
                status = ResponseStatus.ErrorUnknown;
            }
            return new Response<UnitDetail?>(unit, status);
        }

        public Response<UnitDetail?> SetUnit(IConfiguration configuration, IMessageLogger logger, IUnitRepository unitRepository, IPeripheryRepository peripheryRepository, IPubRepository pubRepository, IHistoryActionRepository historyActionRepository, IHistoryActionEditRepository historyActionEditRepository, string handlePub, string deviceId, string setCode, string nfc, bool hasSimcard, List<PeripheryAddItem> peripheries, string sessionId)
        {
            var unitReturn = (UnitDetail?)null;
            var status = ResponseStatus.ErrorUnknown;
            try
            {
                var sessionManager = new SessionManager();

                if (sessionManager.Exists(sessionId, out User? user) &&
                    user != null)
                {
                    var pub = pubRepository.GetByHandle(handlePub);
                    if (pub != null)
                    {
                        var unitOrginal = unitRepository.GetByDeviceId(deviceId, new List<byte>() { (byte)UnitState.New, (byte)UnitState.Montage });

                        //existing unit (remove peripheries and set properties
                        if (unitOrginal != null)
                        {
                            var removedPeripheries = peripheryRepository.RemoveAllUnitPeripheries(unitOrginal.Id);
                            if (unitRepository.CheckNFCDuplicity(unitOrginal.Id, unitOrginal.NFC))
                            {
                                var oldData = unitRepository.CloneById(unitOrginal.Id);

                                var unitEdited = unitRepository.SetUnit(unitOrginal, setCode, nfc, hasSimcard, pub.Id);
                                unitEdited = SetUnitDefaultValues(unitEdited, configuration);

                                var formFields = HistoryHelper.FillFieldsUnit(oldData, unitEdited, pubRepository);
                                var historyActionId = HistoryHelper.HistoryEditAction(historyActionRepository, historyActionEditRepository, user, (byte)FormId.Unit, unitEdited.Id, unitEdited.DeviceId, unitEdited.PubId, (byte)FormId.Pub, formFields, (byte)HistoryActionType.Edit);

                                if (peripheries?.Count > 0)
                                {
                                    AddPeripheries(peripheryRepository, historyActionEditRepository, historyActionRepository, user, unitEdited.Id, unitEdited.DeviceId, peripheries);
                                }
                                unitReturn = GetUnitDetail(peripheryRepository, unitEdited);

                                status = ResponseStatus.OK;
                            }
                            else
                                status = ResponseStatus.ErrorUnitDuplicity;
                        }

                        //new unit
                        else
                        {
                            var unit = unitRepository.GetUnitItem(deviceId, pub.Id, setCode, nfc, hasSimcard);

                            if (unitRepository.CheckUnitDuplicity(unit, 0) && unitRepository.CheckNFCDuplicity(0, unit.NFC))
                            {
                                //get default values
                                unit = SetUnitDefaultValues(unit, configuration);

                                var newId = unitRepository.AddUnit(unit);
                                var formFieldsUnit = HistoryHelper.FillFieldsUnit(unit, pubRepository);
                                var historyActionId = HistoryHelper.HistoryEditAction(historyActionRepository, historyActionEditRepository, user, (byte)FormId.Unit, newId, unit.DeviceId, unit.PubId, (byte)FormId.Pub, formFieldsUnit, (byte)HistoryActionType.Create);

                                if (peripheries?.Count > 0)
                                {
                                    AddPeripheries(peripheryRepository, historyActionEditRepository, historyActionRepository, user, newId, unit.DeviceId, peripheries);
                                }
                                unitReturn = GetUnitDetail(peripheryRepository, unit);

                                status = ResponseStatus.OK;
                            }
                            else
                                status = ResponseStatus.ErrorUnitDuplicity;
                        }
                    }
                    else
                    {
                        status = ResponseStatus.ErrorPubNotFound;
                    }
                }
                else
                    status = ResponseStatus.ErrorUserNotLogged;
            }
            catch (Exception ex)
            {
                logger.Log(configuration, ex);
                status = ResponseStatus.ErrorUnknown;
            }
            return new Response<UnitDetail?>(unitReturn, status);
        }

        private void AddPeripheries(IPeripheryRepository peripheryRepository, IHistoryActionEditRepository historyActionEditRepository, IHistoryActionRepository historyActionRepository, User user, int unitId, string deviceId, List<PeripheryAddItem> peripheries)
        {
            var addedPeriphery = new List<Periphery>();
            var historyTable = new List<HistoryEditField>();
            foreach (var periphery in peripheries)
            {
                var peripheryName = PeripheryConvertor.GetPeripheryNameByExternalType(periphery.Type);
                var item = peripheryRepository.GetPeripheryItem(unitId, PeripheryConvertor.FromExternal(periphery.Type), periphery.SerialNumber, periphery.PressureSensorId, peripheryName);
                addedPeriphery.Add(item);
                historyTable.Add(new HistoryEditField(item.Type, (int)FieldType.Integer, String.Empty, peripheryName));
            }

            peripheryRepository.AddRange(addedPeriphery);
            HistoryHelper.HistoryUnitPeripheriesUpdate(unitId, deviceId, historyTable, user, historyActionEditRepository, historyActionRepository);
        }

        private Unit SetUnitDefaultValues(Unit unit, IConfiguration configuration)
        {
            unit.Name = unit.DeviceId;
            unit.SettingsWifi = false;
            unit.State = (byte)UnitState.New;
            unit.Status = (byte)UnitStatus.Active;
            unit.SanitationStartDate = DateTime.Now.ToUniversalTime();
            unit.SanitationLimit = Config.UnitDefaultSanitationLimit(configuration);
            unit.SanitationSpongesLimit = Config.UnitDefaultSanitationSpongesLimit(configuration);
            unit.FlushLimit = Config.UnitDefaultFlushLimit(configuration);
            unit.WaterLevel = Config.UnitDefaultWaterLevel(configuration);
            unit.Country = "CZ";

            return unit;
        }

        public Response<int> StartService(IConfiguration configuration, IMessageLogger logger, DbContext context, IUnitRepository unitRepository, IServiceRepository serviceRepository, IUserPermissionRepository userPermissionRepository, string deviceId, string sessionId)
        {
            var status = ResponseStatus.ErrorUnknown;
            int serviceId = 0;
            try
            {
                var sessionManager = new SessionManager();

                if (sessionManager.Exists(sessionId, out User? user) &&
                    user != null)
                {
                    if (!userPermissionRepository.HasPermission(user.Id, user.Role, (byte)Permission.Service))
                        status = ResponseStatus.ErrorNoPermission;
                    else
                    {
                        var unit = unitRepository.GetByDeviceId(deviceId);
                        if (unit != null)
                        {
                            UnitProcesses.ChangeUnitState(context, unit, (byte)UnitState.Montage);
                            //response = unitRepository.SetState(deviceId, (byte)UnitState.Montage);

                            serviceId = serviceRepository.Add(unit.Id, user.Id, (byte)ServiceType.Service);
                            status = ResponseStatus.OK;
                        }
                        else
                        {
                            status = ResponseStatus.ErrorUnitNotFound;
                        }
                    }
                }
                else
                    status = ResponseStatus.ErrorUserNotLogged;
            }
            catch (Exception ex)
            {
                logger.Log(configuration, ex);
                status = ResponseStatus.ErrorUnknown;
            }
            return new Response<int>(serviceId, status);
        }

        public Response<string> SuccessService(IConfiguration configuration, IMessageLogger logger, DbContext context, IUnitRepository unitRepository, IServiceRepository serviceRepository, IUserPermissionRepository userPermissionRepository, ICommunicationIODAPIerrorRepository communicationIODAPIerrorRepository, string deviceId, string sessionId, string raAddress, string taskHandle, string userToken)
        {
            var status = ResponseStatus.ErrorUnknown;
            try
            {
                var sessionManager = new SessionManager();

                if (sessionManager.Exists(sessionId, out User? user) &&
                    user != null)
                {
                    if (!userPermissionRepository.HasPermission(user.Id, user.Role, (byte)Permission.Service))
                        status = ResponseStatus.ErrorNoPermission;
                    else
                    {
                        var unit = unitRepository.GetByDeviceId(deviceId);
                        if (unit != null)
                        {
                            var responseFinishService = serviceRepository.FinishService(unit.Id, (byte)ServiceType.Service, true, out int serviceId);
                            if (responseFinishService)
                            {
                                //unitRepository.SetState(deviceId, (byte)UnitState.Active);
                                UnitProcesses.ChangeUnitState(context, unit, (byte)UnitState.Active);

                                status = ResponseStatus.OK;

                                SetTaskOnIODApi(taskHandle, userToken, deviceId, user.Id, (byte)UnitState.Active, communicationIODAPIerrorRepository, raAddress);
                            }
                            else
                            {
                                status = ResponseStatus.ErrorServiceNotFound;
                            }
                        }
                        else
                        {
                            status = ResponseStatus.ErrorUnitNotFound;
                        }
                    }
                }
                else
                    status = ResponseStatus.ErrorUserNotLogged;
            }
            catch (Exception ex)
            {
                logger.Log(configuration, ex);
                status = ResponseStatus.ErrorUnknown;
            }
            return new Response<string>(String.Empty, status);
        }

        public Response<string> CancelService(IConfiguration configuration, IMessageLogger logger, DbContext context, IUnitRepository unitRepository, IServiceRepository serviceRepository, IUserPermissionRepository userPermissionRepository, string deviceId, string sessionId)
        {
            var status = ResponseStatus.ErrorUnknown;
            try
            {
                var sessionManager = new SessionManager();

                if (sessionManager.Exists(sessionId, out User? user) &&
                    user != null)
                {
                    if (!userPermissionRepository.HasPermission(user.Id, user.Role, (byte)Permission.Service))
                        status = ResponseStatus.ErrorNoPermission;
                    else
                    {
                        var unit = unitRepository.GetByDeviceId(deviceId);
                        if (unit != null)
                        {
                            var responseFinishService = serviceRepository.FinishService(unit.Id, (byte)ServiceType.Service, false, out int serviceId);
                            if (responseFinishService)
                            {
                                //unitRepository.SetState(deviceId, (byte)UnitState.Active);
                                UnitProcesses.ChangeUnitState(context, unit, (byte)UnitState.Active);
                                status = ResponseStatus.OK;
                            }
                            else
                            {
                                status = ResponseStatus.ErrorServiceNotFound;
                            }
                        }
                        else
                        {
                            status = ResponseStatus.ErrorUnitNotFound;
                        }
                    }
                }
                else
                    status = ResponseStatus.ErrorUserNotLogged;
            }
            catch (Exception ex)
            {
                logger.Log(configuration, ex);
                status = ResponseStatus.ErrorUnknown;
            }
            return new Response<string>(String.Empty, status);
        }

        public Response<string> StartMontage(IConfiguration configuration, IMessageLogger logger, DbContext context, IUnitRepository unitRepository, IServiceRepository serviceRepository, IUserPermissionRepository userPermissionRepository, ICommunicationIODAPIerrorRepository communicationIODAPIerrorRepository, string deviceId, string raAddress, string taskHandle, string userToken, string sessionId)
        {
            var status = ResponseStatus.ErrorUnknown;
            try
            {
                var sessionManager = new SessionManager();

                if (sessionManager.Exists(sessionId, out User? user) &&
                    user != null)
                {

                    if (!userPermissionRepository.HasPermission(user.Id, user.Role, (byte)Permission.IODTechnology))
                        status = ResponseStatus.ErrorNoPermission;
                    else
                    {
                        var unit = unitRepository.GetByDeviceId(deviceId);
                        if (unit != null)
                        {
                            if (unit.State == (byte)UnitState.Montage)
                            {
                                var responseFinishService = serviceRepository.FinishService(unit.Id, new List<byte> { (byte)ServiceType.Montage }, false, out int serviceId);
                            }
                            else
                            {
                                UnitProcesses.ChangeUnitState(context, unit, (byte)UnitState.Montage);
                            }

                            byte serviceType = unit.State == (byte)UnitState.Active ? (byte)ServiceType.ServiceIOD : (byte)ServiceType.Montage;

                            serviceRepository.Add(unit.Id, user.Id, serviceType);
                            SetTaskStatus(taskHandle, userToken, "initiated", unit.DeviceId, user.Id, (byte)UnitState.Montage, communicationIODAPIerrorRepository, raAddress);
                            status = ResponseStatus.OK;
                        }
                        else
                        {
                            status = ResponseStatus.ErrorUnitNotFound;
                        }
                    }
                }
                else
                    status = ResponseStatus.ErrorUserNotLogged;
            }
            catch (Exception ex)
            {
                logger.Log(configuration, ex);
                status = ResponseStatus.ErrorUnknown;
            }
            return new Response<string>(String.Empty, status);
        }

        public Response<string> SuccessMontage(IConfiguration configuration, IMessageLogger logger, DbContext context, IUnitRepository unitRepository, IServiceRepository serviceRepository, IDeviceSettingsChangeRepository deviceSettingsChangeRepository, IUserPermissionRepository userPermissionRepository, ICommunicationIODAPIerrorRepository communicationIODAPIerrorRepository, string deviceId, string data, int interval, string sessionId, string raAddress, string taskHandle, string userToken)
        {
            var status = ResponseStatus.ErrorUnknown;
            try
            {
                var sessionManager = new SessionManager();

                if (sessionManager.Exists(sessionId, out User? user) &&
                    user != null)
                {
                    if (!userPermissionRepository.HasPermission(user.Id, user.Role, (byte)Permission.IODTechnology))
                        status = ResponseStatus.ErrorNoPermission;
                    else
                    {
                        var unit = unitRepository.GetByDeviceId(deviceId);
                        if (unit != null)
                        {
                            var responseFinishService = serviceRepository.FinishService(unit.Id, new List<byte> { (byte)ServiceType.ServiceIOD, (byte)ServiceType.Montage }, true, out int serviceId);
                            if (responseFinishService)
                            {
                                //unitRepository.SetState(deviceId, (byte)UnitState.Active);
                                UnitProcesses.ChangeUnitState(context, unit, (byte)UnitState.Active);
                                deviceSettingsChangeRepository.AddRecord(deviceId, data);
                                unitRepository.SetSettings(deviceId: deviceId, interval: interval);
                                status = ResponseStatus.OK;
                                SetTaskOnIODApi(taskHandle, userToken, deviceId, user.Id, (byte)UnitState.Active, communicationIODAPIerrorRepository, raAddress);
                            }
                            else
                            {
                                status = ResponseStatus.ErrorServiceNotFound;
                            }
                        }
                        else
                        {
                            status = ResponseStatus.ErrorUnitNotFound;
                        }
                    }
                }
                else
                    status = ResponseStatus.ErrorUserNotLogged;
            }
            catch (Exception ex)
            {
                logger.Log(configuration, ex);
                status = ResponseStatus.ErrorUnknown;
            }
            return new Response<string>(String.Empty, status);
        }

        public Response<string> CancelMontage(IConfiguration configuration, IMessageLogger logger, DbContext context, IUnitRepository unitRepository, IServiceRepository serviceRepository, IDeviceSettingsChangeRepository deviceSettingsChangeRepository, IUserPermissionRepository userPermissionRepository, string deviceId, string data, int interval, string sessionId)
        {
            var status = ResponseStatus.ErrorUnknown;
            try
            {
                var sessionManager = new SessionManager();

                if (sessionManager.Exists(sessionId, out User? user) &&
                    user != null)
                {
                    if (!userPermissionRepository.HasPermission(user.Id, user.Role, (byte)Permission.IODTechnology))
                        status = ResponseStatus.ErrorNoPermission;
                    else
                    {
                        var unit = unitRepository.GetByDeviceId(deviceId);
                        if (unit != null)
                        {
                            var responseFinishService = serviceRepository.FinishService(unit.Id, new List<byte> { (byte)ServiceType.Montage, (byte)ServiceType.ServiceIOD }, false, out int serviceId);
                            if (responseFinishService)
                            {
                                //unitRepository.SetState(deviceId, (byte)UnitState.New);
                                UnitProcesses.ChangeUnitState(context, unit, (byte)UnitState.New);
                                deviceSettingsChangeRepository.AddRecord(deviceId, data);
                                unitRepository.SetSettings(deviceId: deviceId, interval: interval);
                                status = ResponseStatus.OK;
                            }
                            else
                            {
                                status = ResponseStatus.ErrorServiceNotFound;
                            }
                        }
                        else
                        {
                            status = ResponseStatus.ErrorUnitNotFound;
                        }
                    }
                }
                else
                    status = ResponseStatus.ErrorUserNotLogged;
            }
            catch (Exception ex)
            {
                logger.Log(configuration, ex);
                status = ResponseStatus.ErrorUnknown;
            }
            return new Response<string>(String.Empty, status);
        }

        public Response<string> SetUnitLowInterval(IConfiguration configuration, IMessageLogger logger, IUnitRepository unitRepository, IDeviceSettingsChangeRepository deviceSettingsChangeRepository, IUserPermissionRepository userPermissionRepository, string deviceId, string data, int interval, string sessionId)
        {
            var status = ResponseStatus.ErrorUnknown;
            try
            {
                var sessionManager = new SessionManager();

                if (sessionManager.Exists(sessionId, out User? user) &&
                    user != null)
                {
                    if (!userPermissionRepository.HasPermission(user.Id, user.Role, (byte)Permission.IODTechnology))
                        status = ResponseStatus.ErrorNoPermission;
                    else
                    {
                        deviceSettingsChangeRepository.AddRecord(deviceId, data);
                        unitRepository.SetSettings(deviceId: deviceId, interval: interval);
                        status = ResponseStatus.OK;
                    }
                }
                else
                    status = ResponseStatus.ErrorUserNotLogged;
            }
            catch (Exception ex)
            {
                logger.Log(configuration, ex);
                status = ResponseStatus.ErrorUnknown;
            }
            return new Response<string>(string.Empty,
                    status);
        }

        public Response<string> SetSettings(IConfiguration configuration, IMessageLogger logger, IUnitRepository unitRepository, IDeviceSettingsChangeRepository deviceSettingsChangeRepository, IUserPermissionRepository userPermissionRepository, string deviceId, string data, bool? wifi, string? SSID, string? password, string sessionId)
        {
            var status = ResponseStatus.ErrorUnknown;
            try
            {
                var sessionManager = new SessionManager();

                if (sessionManager.Exists(sessionId, out User? user) &&
                    user != null)
                {
                    if (!userPermissionRepository.HasPermission(user.Id, user.Role, (byte)Permission.IODTechnology))
                        status = ResponseStatus.ErrorNoPermission;
                    else
                    {
                        deviceSettingsChangeRepository.AddRecord(deviceId, data);
                        var res = unitRepository.SetSettings(deviceId: deviceId, wifi: wifi, SSID: SSID, password: password);
                        if (res > 0)
                            status = ResponseStatus.OK;
                        else
                            status = ResponseStatus.ErrorUnitNotFound;
                    }
                }
                else
                    status = ResponseStatus.ErrorUserNotLogged;
            }
            catch (Exception ex)
            {
                logger.Log(configuration, ex);
                status = ResponseStatus.ErrorUnknown;
            }
            return new Response<string>(string.Empty,
                    status);
        }

        public Response<PeripheryLocalizationInfo?> GetPeripheryLocalization(IConfiguration configuration, IMessageLogger logger, IPeripheryLocalizationRepository peripheryLocalizationRepository, int peripheryId, string sessionId)
        {
            var status = ResponseStatus.ErrorUnknown;
            PeripheryLocalizationInfo? localization = null;
            try
            {
                var sessionManager = new SessionManager();

                if (sessionManager.Exists(sessionId, out User? user) && user != null)
                {
                    var localizationDB = peripheryLocalizationRepository.GetPeripheryLocalization(peripheryId);

                    if (localizationDB != null)
                    {
                        localization = new PeripheryLocalizationInfo()
                        {
                            SerialNumber = localizationDB.SerialNumber,
                            Slot = localizationDB.Slot,
                            HubSerialNumber = localizationDB.HubSerialNumber,
                            HubSlot = localizationDB.HubSlot,
                        };
                        status = ResponseStatus.OK;
                    }
                    else
                        status = ResponseStatus.ErrorPeripheryNotFound;
                }
                else
                    status = ResponseStatus.ErrorUserNotLogged;
            }
            catch (Exception ex)
            {
                logger.Log(configuration, ex);
                status = ResponseStatus.ErrorUnknown;
            }
            return new Response<PeripheryLocalizationInfo?>(localization, status);
        }

        public Response<string> SetTapUnitOrder(IConfiguration configuration, IMessageLogger logger, ITapUnitOrderRepository tapUnitOrderRepository, IUserPermissionRepository userPermissionRepository, IPeripheryRepository peripheryRepository, int peripheryId, int unitId, int order, string sessionId)
        {
            var status = ResponseStatus.ErrorUnknown;
            try
            {
                var sessionManager = new SessionManager();

                if (sessionManager.Exists(sessionId, out User? user) && user != null)
                {
                    if (!userPermissionRepository.HasPermission(user.Id, user.Role, (byte)Permission.IODTechnology))
                        status = ResponseStatus.ErrorNoPermission;
                    else
                    {
                        tapUnitOrderRepository.AddRecord(unitId, peripheryId, order);
                        var orderName = order + 1;
                        var name = Config.TapGeneratedName(configuration) + " " + orderName.ToString();
                        peripheryRepository.SetPeripheryName(unitId, peripheryId, name);
                        status = ResponseStatus.OK;
                    }
                }
                else
                    status = ResponseStatus.ErrorUserNotLogged;
            }
            catch (Exception ex)
            {
                logger.Log(configuration, ex);
                status = ResponseStatus.ErrorUnknown;
            }
            return new Response<string>(String.Empty, status);
        }

        public Response<Dictionary<int, decimal>?> GetUnitPeripheriesData(IConfiguration configuration, IMessageLogger logger, IUnitPeripheriesDataFluidRepository unitPeripheriesDataFluidRepository, int unitId, DateTime from, string sessionId)
        {
            var status = ResponseStatus.ErrorUnknown;
            var response = (Dictionary<int, decimal>?)null;

            try
            {
                var sessionManager = new SessionManager();

                if (sessionManager.Exists(sessionId, out User? user) && user != null)
                {
                    response = unitPeripheriesDataFluidRepository.GetUnitPeripheriesData(unitId, from);
                    status = response != null ? ResponseStatus.OK : ResponseStatus.ErrorNoData;
                }
                else
                    status = ResponseStatus.ErrorUserNotLogged;
            }
            catch (Exception ex)
            {
                logger.Log(configuration, ex);
                status = ResponseStatus.ErrorUnknown;
            }

            return new Response<Dictionary<int, decimal>?>(response, status);
        }

        public Response<string> SetServiceNote(IConfiguration configuration, IMessageLogger logger, IUnitRepository unitRepository, IServiceRepository serviceRepository, IUserPermissionRepository userPermissionRepository, string deviceId, string note, string sessionId)
        {
            var status = ResponseStatus.ErrorUnknown;

            try
            {
                var sessionManager = new SessionManager();

                if (sessionManager.Exists(sessionId, out User? user) &&
                    user != null)
                {
                    if (!userPermissionRepository.HasPermission(user.Id, user.Role, (byte)Permission.Service))
                        status = ResponseStatus.ErrorNoPermission;
                    else
                    {
                        var unit = unitRepository.GetByDeviceId(deviceId);
                        if (unit != null)
                        {
                            //var responseFinishService = serviceRepository.FinishService(unit.Id, (byte)ServiceType.Service, false, out int serviceId);
                            //if (responseFinishService)
                            //{
                            if (note.Length <= 500)
                            {
                                if (serviceRepository.AddNote(unit.Id, (byte)ServiceType.Service, note))
                                    status = ResponseStatus.OK;
                                else
                                    status = ResponseStatus.ErrorUnitNotFound;
                            }
                            else
                            {
                                status = ResponseStatus.ErrorSrviceNoteOutOfRange;
                            }
                            //}
                            //else
                            //{
                            //    status = ResponseStatus.ErrorServiceNotFound;
                            //}
                        }
                        else
                        {
                            status = ResponseStatus.ErrorUnitNotFound;
                        }
                    }
                }
                else
                    status = ResponseStatus.ErrorUserNotLogged;
            }
            catch (Exception ex)
            {
                logger.Log(configuration, ex);
                status = ResponseStatus.ErrorUnknown;
            }
            return new Response<string>(String.Empty, status);
        }

        public Response<string> AddPicture(IConfiguration configuration, IMessageLogger logger, IUserPermissionRepository userPermissionRepository, IUnitServicePictureRepository unitServicePictureRepository, int serviceId, byte component, byte pictureType, byte[]? filedata, string filename, string sessionId)
        {
            var status = ResponseStatus.ErrorUnknown;
            try
            {
                var sessionManager = new SessionManager();

                if (sessionManager.Exists(sessionId, out User? user) &&
                    user != null)
                {
                    if (!userPermissionRepository.HasPermission(user.Id, user.Role, (byte)Permission.Service))
                        status = ResponseStatus.ErrorNoPermission;
                    else
                    {
                        if (filedata != null)
                        {
                            var data = filedata;

                            var filesize = filedata.Length;

                            unitServicePictureRepository.AddPicture(serviceId, component, pictureType, filename, filesize, filedata);
                            status = ResponseStatus.OK;
                        }
                        else
                        {
                            status = ResponseStatus.ErrorNoFileData;
                        }
                    }
                }
                else
                    status = ResponseStatus.ErrorUserNotLogged;
            }
            catch (Exception ex)
            {
                logger.Log(configuration, ex);
                status = ResponseStatus.ErrorUnknown;
            }
            return new Response<string>(String.Empty, status);
        }

        public Response<UnitDetail?> GetUnitByNFC(IConfiguration configuration, IMessageLogger logger, IUnitRepository unitRepository, IPeripheryRepository peripheryRepository, string NFC, string sessionId)
        {
            var status = ResponseStatus.ErrorUnknown;
            UnitDetail? unit = null;

            try
            {
                var sessionManager = new SessionManager();

                if (sessionManager.Exists(sessionId, out User? user) &&
                    user != null)
                {
                    var unitDB = unitRepository.GetUnitByNFC(NFC);
                    if (unitDB != null)
                    {
                        unit = GetUnitDetail(peripheryRepository, unitDB);
                        status = unit == null ? ResponseStatus.ErrorUnitNotFound : ResponseStatus.OK;
                    }
                    else
                        status = ResponseStatus.ErrorUnitNotFound;

                }
                else
                    status = ResponseStatus.ErrorUserNotLogged;
            }
            catch (Exception ex)
            {
                logger.Log(configuration, ex);
                status = ResponseStatus.ErrorUnknown;
            }
            return new Response<UnitDetail?>(unit, status);
        }

        public Response<List<string>?> LogIOD(IConfiguration configuration, IMessageLogger logger, IUserPermissionRepository userPermissionRepository, IUnitRepository unitRepository, IUnitEventRepository unitEventRepository, string deviceId, DateTime from, string sessionId)
        {
            List<string>? response = null;
            var status = ResponseStatus.ErrorUnknown;

            try
            {
                var sessionManager = new SessionManager();

                if (sessionManager.Exists(sessionId, out User? user) && user != null)
                {
                    if (!userPermissionRepository.HasPermission(user.Id, user.Role, (byte)Permission.IODTechnology))
                        status = ResponseStatus.ErrorNoPermission;
                    else
                    {
                        var unit = unitRepository.GetByDeviceId(deviceId);
                        if (unit == null)
                            status = ResponseStatus.ErrorUnitNotFound;
                        else
                        {
                            var rm = new Resx.ResMgr("SmartSense.API.ServiceModule.Resx.AlertTexts");
                            var culture = (UserCulture)user.Culture;

                            var logEventIdTypes = new HashSet<byte>() { 5, 6, 7 };
                            var items = unitEventRepository.GetByDeviceId(deviceId, from);
                            items = items.Where(o => logEventIdTypes.Contains(o.EventId)).ToList();

                            response = new List<string>();
                            foreach (var item in items)
                            {
                                if (item.EventId == 7)
                                    response.Add(rm.GetString($"alert_log_7", culture).Replace("$id;", item.Data));
                                else
                                    response.Add(rm.GetString($"alert_log_{item.EventId}_{item.Data}", culture));
                            }
                            status = ResponseStatus.OK;
                        }
                    }
                }
                else
                    status = ResponseStatus.ErrorUserNotLogged;
            }
            catch (Exception ex)
            {
                logger.Log(configuration, ex);
                status = ResponseStatus.ErrorUnknown;
            }

            return new Response<List<string>?>(response, status);
        }

        public Response<CheckIODResponse?> CheckIOD(IConfiguration configuration, IMessageLogger logger, IUserPermissionRepository userPermissionRepository,
            IUnitRepository unitRepository, IPeripheryRepository peripheryRepository, IHubRepository hubRepository,
            string deviceId, DateTime from, string sessionId)
        {
            CheckIODResponse? response = null;
            var status = ResponseStatus.ErrorUnknown;

            try
            {
                var sessionManager = new SessionManager();

                if (sessionManager.Exists(sessionId, out User? user) && user != null)
                {
                    if (!userPermissionRepository.HasPermission(user.Id, user.Role, (byte)Permission.IODTechnology))
                        status = ResponseStatus.ErrorNoPermission;
                    else
                    {
                        var unit = unitRepository.GetByDeviceId(deviceId);
                        if (unit == null)
                            status = ResponseStatus.ErrorUnitNotFound;
                        else if (unit != null && unit.Topology == null)
                        {
                            //no topology has been recieved yet, we need to return info about it
                            var peripheries = peripheryRepository.GetUnitPeripheries(unit.Id)?.ToList() ?? new List<Periphery>();
                            response = new CheckIODResponse()
                            {
                                Peripheries = IODPeriphery.Create(peripheries, IODPeripheryState.NoTopologyRecieved),
                                LastDataRecieved = peripheryRepository.GetLastDataRecievedByDeviceId(deviceId)
                            };

                            status = ResponseStatus.OK;
                        }
                        else if (unit != null && unit.Topology != null)
                        {
                            //last topology server recieved
                            var sData = Encoding.UTF8.GetString(unit.Topology);
                            var topologyData = JsonSerializer.Deserialize<TopologyData>(sData);

                            //set peripheries in db
                            var peripheries = peripheryRepository.GetUnitPeripheries(unit.Id)?.ToList();

                            //topology must be set
                            if (topologyData != null && peripheries != null)
                            {
                                //create topology and return states tested according to topology
                                var topology = new Topology(topologyData);
                                var pers = topology.CheckPeripheries(peripheries, out bool allPaired);
                                //if all thermometers were paired we can save topology types to db
                                if (allPaired)
                                    topology.SavePairedThermometers(peripheryRepository, unit);
                                //save topology to peripheries in db (we need it because of sending data)
                                topology.SetUnitByTopology(peripheryRepository, hubRepository, topologyData, unit);
                                //check if data are recieved for peripheries
                                pers = topology.CheckData(peripheryRepository, pers, deviceId, from);

                                //create response
                                response = new CheckIODResponse();
                                response.Peripheries = pers;
                                response.LastDataRecieved = peripheryRepository.GetLastDataRecievedByDeviceId(deviceId);

                                //method processed ok
                                status = ResponseStatus.OK;
                            }
                        }
                    }
                }
                else
                    status = ResponseStatus.ErrorUserNotLogged;
            }
            catch (Exception ex)
            {
                logger.Log(configuration, ex);
                status = ResponseStatus.ErrorUnknown;
            }

            if (response != null)
                response.ConvertPeripheriesToExternal();

            return new Response<CheckIODResponse?>(response, status);
        }

        public Response<ConfirmIODResponse?> ConfirmIOD(IConfiguration configuration, IMessageLogger logger, IUserPermissionRepository userPermissionRepository,
            IUnitRepository unitRepository, IPeripheryRepository peripheryRepository, IHubRepository hubRepository,
            string deviceId, DateTime from, string sessionId)
        {
            bool success = false;
            ResponseStatus status;
            CheckIODResponse? checkResponse = null;

            try
            {
                var sessionManager = new SessionManager();

                if (sessionManager.Exists(sessionId, out User? user) && user != null)
                {
                    if (!userPermissionRepository.HasPermission(user.Id, user.Role, (byte)Permission.IODTechnology))
                        status = ResponseStatus.ErrorNoPermission;
                    else
                    {
                        //get unit
                        var unit = unitRepository.GetByDeviceId(deviceId);
                        //we need topology
                        if (unit?.Topology != null)
                        {
                            //check if topology is correct
                            var _checkResponse = CheckIOD(configuration, logger, userPermissionRepository, unitRepository, peripheryRepository, hubRepository, deviceId, from, sessionId);

                            //method for check run ok
                            if (_checkResponse != null && _checkResponse.Status == ResponseStatus.OK)
                            {
                                //response data
                                checkResponse = _checkResponse.Data;
                                //we need valid response
                                if (checkResponse != null && checkResponse.IsValid())
                                {
                                    //last topology server recieved
                                    var sData = Encoding.UTF8.GetString(unit.Topology);
                                    var topologyData = JsonSerializer.Deserialize<TopologyData>(sData);

                                    //valid topology data
                                    if (topologyData != null)
                                    {
                                        //requirements are ok
                                        //set unit peripheries by recieved topology
                                        var topology = new Topology(topologyData);
                                        success = topology.SetUnitByTopology(peripheryRepository, hubRepository, topologyData, unit);
                                    }
                                }
                            }
                        }

                        status = ResponseStatus.OK;
                    }
                }
                else
                    status = ResponseStatus.ErrorUserNotLogged;
            }
            catch (Exception ex)
            {
                logger.Log(configuration, ex);
                status = ResponseStatus.ErrorUnknown;
            }
            return new Response<ConfirmIODResponse?>(new ConfirmIODResponse()
            {
                Success = success,
                CheckResponse = checkResponse
            }, status);
        }

        public Response<string> AddServiceComponents(IConfiguration configuration, IMessageLogger logger, IServiceRepository serviceRepository, IUserPermissionRepository userPermissionRepository, IServiceComponentRepository serviceComponentRepository, ITapUnitOrderRepository tapUnitOrderRepository, IUnitRepository unitRepository, IPubApiInfoRepository pubApiInfoRepository, IComponentPriceRepository componentPriceRepository, int unitId, List<byte> components, string sessionId)
        {
            var status = ResponseStatus.ErrorUnknown;
            try
            {

                var sessionManager = new SessionManager();

                if (sessionManager.Exists(sessionId, out User? user) &&
                    user != null)
                {
                    if (!userPermissionRepository.HasPermission(user.Id, user.Role, (byte)Permission.Service))
                        status = ResponseStatus.ErrorNoPermission;
                    else
                    {
                        var service = serviceRepository.GetOpenServiceId(unitId, (byte)ServiceType.Service);
                        if (service == null)
                        {
                            status = ResponseStatus.ErrorServiceNotFound;
                        }
                        else
                        {
                            var unit = unitRepository.GetById(service.UnitId);
                            if (unit == null || unit.PubId == null)
                                status = ResponseStatus.ErrorUnitNotFound;
                            else
                            {
                                var pub = pubApiInfoRepository.GetById(unit.PubId.Value);

                                var pricedComponents = componentPriceRepository.GetComponentsPrices(components, pub?.BreweryId, pub?.Currency);

                                serviceComponentRepository.AddServiceComponents(service.Id, pricedComponents);

                                //if components contains python, do unpairing flowmeters
                                if (components.Contains((byte)Components.Python))
                                    tapUnitOrderRepository.RemoveRecord(unitId);

                                status = ResponseStatus.OK;
                            }
                        }
                    }
                }
                else
                    status = ResponseStatus.ErrorUserNotLogged;
            }
            catch (Exception ex)
            {
                logger.Log(configuration, ex);
                status = ResponseStatus.ErrorUnknown;
            }
            return new Response<string>(String.Empty, status);
        }

        public Response<AlertsList> GetAllAlertMessages(IConfiguration configuration, IMessageLogger logger, IUserPermissionRepository userPermissionRepository, IPeripheryRepository peripheryRepository, IAlertMessageInfoRepository alertMessageInfoRepository, IUnitRepository unitRepository, int unitId, string sessionId)
        {
            var status = ResponseStatus.ErrorUnknown;
            var response = new AlertsList();
            response.List = new List<AlertItem>();

            try
            {
                var sessionManager = new SessionManager();

                if (sessionManager.Exists(sessionId, out User? user) &&
                    user != null)
                {
                    if (!userPermissionRepository.HasPermission(user.Id, user.Role, (byte)Permission.Service))
                        status = ResponseStatus.ErrorNoPermission;
                    else
                    {
                        //check if unit exist
                        var unit = unitRepository.GetById(unitId);
                        if (unit != null)
                        {
                            var alertMessages = alertMessageInfoRepository.GetAlerts(unitId, DateTime.UtcNow.AddDays(-7))?.ToList() ?? new List<AlertMessageInfo>();
                            var data = new List<AlertItem>();
                            var culture = (UserCulture)user.Culture;

                            foreach (var message in alertMessages)
                            {
                                if (message != null)
                                {
                                    var alertType = (AlertType)message.Type;
                                    AlertTexterProvider.GetAlertMessageTexts(culture, alertType, message?.Data ?? String.Empty,
                                        out string subject, out string body, out string peripheryName, out AlertMessageDataErrorType messageErrorType);
                                    data.Add(new AlertItem()
                                    {
                                        Date = message?.Date ?? DateTime.UtcNow,
                                        ErrorType = (byte)messageErrorType,
                                        PeripheryId = message?.PeripheryId ?? 0,
                                        SerialNumber = message?.DeviceId ?? string.Empty,
                                        Type = (byte)alertType,
                                        State = message?.State ?? 0,
                                        Description = body
                                    });
                                }
                            }
                            response.List = data;
                            status = ResponseStatus.OK;
                        }
                        else
                        {
                            status = ResponseStatus.ErrorUnitNotFound;
                        }
                    }
                }
                else
                    status = ResponseStatus.ErrorUserNotLogged;
            }
            catch (Exception ex)
            {
                logger.Log(configuration, ex);
                status = ResponseStatus.ErrorUnknown;
            }
            return new Response<AlertsList>(response, status);
        }

        public Response<AlertsList> GetAlertMessages(IConfiguration configuration, IMessageLogger logger, IUserPermissionRepository userPermissionRepository, IPeripheryRepository peripheryRepository, IAlertMessageInfoRepository alertMessageInfoRepository, IUnitRepository unitRepository, int unitId, string sessionId)
        {
            var status = ResponseStatus.ErrorUnknown;
            var response = new AlertsList();
            response.List = new List<AlertItem>();

            try
            {
                var sessionManager = new SessionManager();

                if (sessionManager.Exists(sessionId, out User? user) &&
                    user != null)
                {
                    if (!userPermissionRepository.HasPermission(user.Id, user.Role, (byte)Permission.Service))
                        status = ResponseStatus.ErrorNoPermission;
                    else
                    {
                        //check if unit exist
                        var unit = unitRepository.GetById(unitId);
                        if (unit != null)
                        {
                            var unitPeripheries = peripheryRepository.GetUnitPeripheries(unitId)?.Select(p => p.Id).ToList();
                            var alertMessages = alertMessageInfoRepository?.GetAlerts(unitId, DateTime.UtcNow.AddDays(-7))?.ToList() ?? new List<AlertMessageInfo>();

                            var data = new List<AlertItem>();
                            var culture = (UserCulture)user.Culture;

                            foreach (var message in alertMessages)
                            {
                                if (message != null)
                                {
                                    var alertType = (AlertType)message.Type;
                                    AlertTexterProvider.GetAlertMessageTexts(culture, alertType, message?.Data ?? String.Empty,
                                        out string subject, out string body, out string peripheryName, out AlertMessageDataErrorType messageErrorType);
                                    data.Add(new AlertItem()
                                    {
                                        Date = message?.Date ?? DateTime.UtcNow,
                                        ErrorType = (byte)messageErrorType,
                                        PeripheryId = message?.PeripheryId ?? 0,
                                        SerialNumber = message?.DeviceId ?? string.Empty,
                                        Type = (byte)alertType,
                                        State = message?.State ?? 0,
                                        Description = body
                                    });
                                }
                            }

                            //unitAlerts
                            var unitAlerts = data.Where(p => p.PeripheryId == null);
                            if (unitAlerts.Any())
                            {
                                var lastUnitAlert = unitAlerts.Where(p => p.Type == (byte)AlertType.TransferingUnit)?.OrderByDescending(t => t.Date).FirstOrDefault();
                                if (lastUnitAlert != null && lastUnitAlert.ErrorType != (byte)AlertMessageDataErrorType.OK)
                                {
                                    response.List.Add(lastUnitAlert);
                                }

                                var logAlerts = unitAlerts.Where(p => p.Type == (byte)AlertType.LogInfo);
                                if (logAlerts.Any())
                                    response.List.AddRange(logAlerts);
                            }

                            if (unitPeripheries != null && unitPeripheries.Any())
                            {
                                //peripheryAlerts
                                foreach (var peripheryId in unitPeripheries)
                                {
                                    var lastPeripheryTransferAlert = unitAlerts.Where(p => p.PeripheryId == peripheryId && p.Type == (byte)AlertType.TransferingPeriphery)?.OrderByDescending(t => t.Date).FirstOrDefault();
                                    if (lastPeripheryTransferAlert != null && lastPeripheryTransferAlert.ErrorType != (byte)AlertMessageDataErrorType.OK)
                                    {
                                        response.List.Add(lastPeripheryTransferAlert);
                                    }

                                    var lastPeripheryMonitoringAlert = unitAlerts.Where(p => p.PeripheryId == peripheryId && p.Type == (byte)AlertType.MonitoringPeriphery)?.OrderByDescending(t => t.Date).FirstOrDefault();
                                    if (lastPeripheryMonitoringAlert != null && lastPeripheryMonitoringAlert.ErrorType != (byte)AlertMessageDataErrorType.OK)
                                    {
                                        response.List.Add(lastPeripheryMonitoringAlert);
                                    }
                                }
                            }
                            status = ResponseStatus.OK;
                        }
                        else
                        {
                            status = ResponseStatus.ErrorUnitNotFound;
                        }
                    }
                }
                else
                    status = ResponseStatus.ErrorUserNotLogged;
            }
            catch (Exception ex)
            {
                logger.Log(configuration, ex);
                status = ResponseStatus.ErrorUnknown;
            }
            return new Response<AlertsList>(response, status);
        }

        public Response<string> DisassemblyUnit(IConfiguration configuration, IMessageLogger logger, DbContext context, IUserPermissionRepository userPermissionRepository, IUnitRepository unitRepository, IServiceRepository serviceRepository, IPubRepository pubRepository, IHistoryActionRepository historyActionRepository, IHistoryActionEditRepository historyActionEditRepository, int unitId, string sessionId)
        {
            var status = ResponseStatus.ErrorUnknown;

            try
            {
                var sessionManager = new SessionManager();

                if (sessionManager.Exists(sessionId, out User? user) &&
                    user != null)
                {
                    if (!userPermissionRepository.HasPermission(user.Id, user.Role, (byte)Permission.IODTechnology))
                        status = ResponseStatus.ErrorNoPermission;
                    else
                    {
                        //check if unit exist
                        var unit = unitRepository.GetById(unitId);
                        if (unit != null)
                        {
                            UnitProcesses.DisassemblyUnit(context, unit);

                            //Adding copy of unit and putting it into history
                            int pubSkladId = Config.PubSkladId(configuration);
                            var unitCopy = unitRepository.AddUnitCopy(unit, pubSkladId);
                            var formFieldsUnit = HistoryHelper.FillFieldsUnit(unitCopy, pubRepository);
                            var historyActionId = HistoryHelper.HistoryEditAction(historyActionRepository, historyActionEditRepository, user, (byte)FormId.Unit, unitCopy.Id, unit.DeviceId, pubSkladId, (byte)FormId.Pub, formFieldsUnit, (byte)HistoryActionType.Create);

                            serviceRepository.Add(unit.Id, user.Id, (byte)ServiceType.Demontage);
                            status = ResponseStatus.OK;
                        }
                        else
                        {
                            status = ResponseStatus.ErrorUnitNotFound;
                        }
                    }
                }
                else
                    status = ResponseStatus.ErrorUserNotLogged;
            }
            catch (Exception ex)
            {
                logger.Log(configuration, ex);
                status = ResponseStatus.ErrorUnknown;
            }
            return new Response<string>(string.Empty, status);
        }

        public Response<string> ChangeUnitState(IConfiguration configuration, IMessageLogger logger, DbContext context, IUserPermissionRepository userPermissionRepository, IUnitRepository unitRepository, int unitId, byte unitState, string sessionId)
        {
            var status = ResponseStatus.ErrorUnknown;

            var sessionManager = new SessionManager();

            try
            {
                if (sessionManager.Exists(sessionId, out User? user) &&
                    user != null)
                {
                    if (!userPermissionRepository.HasPermission(user.Id, user.Role, (byte)Permission.Service))
                        status = ResponseStatus.ErrorNoPermission;
                    else
                    {
                        //check if unit exist
                        var unit = unitRepository.GetById(unitId);
                        if (unit != null)
                        {
                            UnitProcesses.DisassemblyUnit(context, unit);
                            status = ResponseStatus.OK;
                        }
                        else
                        {
                            status = ResponseStatus.ErrorUnitNotFound;
                        }
                    }
                }
                else
                    status = ResponseStatus.ErrorUserNotLogged;
            }
            catch (Exception ex)
            {
                logger.Log(configuration, ex);
                status = ResponseStatus.ErrorUnknown;
            }
            return new Response<string>(string.Empty, status);
        }

        public Response<string> ExchangeUnit(IConfiguration configuration, IMessageLogger logger, DbContext context, IUserPermissionRepository userPermissionRepository, IUnitRepository unitRepository, int oldUnitId, int newUnitId, string sessionId)
        {
            var status = ResponseStatus.ErrorUnknown;

            try
            {
                var sessionManager = new SessionManager();

                if (sessionManager.Exists(sessionId, out User? user) &&
                    user != null)
                {
                    if (!userPermissionRepository.HasPermission(user.Id, user.Role, (byte)Permission.Service))
                        status = ResponseStatus.ErrorNoPermission;
                    else
                    {
                        //check if units exist
                        var unitOld = unitRepository.GetById(oldUnitId);
                        var unitNew = unitRepository.GetById(newUnitId);
                        if (unitOld != null && unitNew != null)
                        {
                            UnitProcesses.ExchangeUnit(context, newUnitId, oldUnitId);
                            status = ResponseStatus.OK;
                        }
                        else
                        {
                            status = ResponseStatus.ErrorUnitNotFound;
                        }
                    }
                }
                else
                    status = ResponseStatus.ErrorUserNotLogged;
            }
            catch (Exception ex)
            {
                logger.Log(configuration, ex);
                status = ResponseStatus.ErrorUnknown;
            }
            return new Response<string>(string.Empty, status);
        }

        public Response<string> ChangePeriphery(IConfiguration configuration, IMessageLogger logger, DbContext context, IUserPermissionRepository userPermissionRepository, IPeripheryRepository peripheryRepository, int peripheryId, string serialNumber, string sessionId)
        {
            var status = ResponseStatus.ErrorUnknown;

            try
            {
                var sessionManager = new SessionManager();

                if (sessionManager.Exists(sessionId, out User? user) &&
                    user != null)
                {
                    if (!userPermissionRepository.HasPermission(user.Id, user.Role, (byte)Permission.Service))
                        status = ResponseStatus.ErrorNoPermission;
                    else
                    {
                        //check if periphery exist

                        var periphery = peripheryRepository.GetById(peripheryId);
                        if (periphery != null)
                        {
                            PeripheryProcesses.ChangePeriphery(context, periphery, serialNumber);
                            status = ResponseStatus.OK;
                        }
                        else
                        {
                            status = ResponseStatus.ErrorPeripheryNotFound;
                        }
                    }
                }
                else
                    status = ResponseStatus.ErrorUserNotLogged;
            }
            catch (Exception ex)
            {
                logger.Log(configuration, ex);
                status = ResponseStatus.ErrorUnknown;
            }
            return new Response<string>(string.Empty, status);
        }

        public Response<UnitDetail?> RemovePeripheries(IConfiguration configuration, IMessageLogger logger, IUnitRepository unitRepository, IPubRepository pubRepository, IPeripheryRepository peripheryRepository, IHistoryActionRepository historyActionRepository, IHistoryActionEditRepository historyActionEditRepository, string handlePub, string deviceId, List<PeripheryAddItem>? peripheriesToRemove, string sessionId)
        {
            var unitReturn = (UnitDetail?)null;
            var status = ResponseStatus.OK;
            try
            {
                var sessionManager = new SessionManager();

                if (sessionManager.Exists(sessionId, out User? user) &&
                    user != null)
                {
                    var pub = pubRepository.GetByHandle(handlePub);
                    if (pub != null)
                    {
                        var unit = unitRepository.GetByDeviceId(deviceId);
                        if (unit != null)
                        {
                            if (peripheriesToRemove != null && peripheriesToRemove.Any())
                            {
                                foreach (var periphery in peripheriesToRemove)
                                {
                                    periphery.Type = PeripheryConvertor.FromExternal(periphery.Type);
                                }
                                var historyTable = new List<HistoryEditField>();

                                var pers = GetPeripheriesItems(peripheryRepository, peripheriesToRemove, unit);
                                peripheryRepository.RemovePeripheries(pers);

                                foreach (var item in pers)
                                {
                                    if (item != null)
                                    {
                                        int fieldId = PeripheryConvertor.GetPeripheryFieldId(item.Type);

                                        historyTable.Add(new HistoryEditField(fieldId, (int)FieldType.Integer, item?.Name ?? string.Empty, String.Empty));
                                    }
                                }

                                if (historyTable.Count > 0)
                                {
                                    HistoryUnitPeripheriesUpdate(historyActionRepository, historyActionEditRepository, unit.Id, unit.Name, historyTable, user);
                                }


                            }
                        }
                        else
                        {
                            status = ResponseStatus.ErrorUnitNotFound;
                        }
                    }
                    else
                    {
                        status = ResponseStatus.ErrorPubNotFound;
                    }
                }
                else
                    status = ResponseStatus.ErrorUserNotLogged;
            }
            catch (Exception ex)
            {
                logger.Log(configuration, ex);
                status = ResponseStatus.ErrorUnknown;
            }
            return new Response<UnitDetail?>(unitReturn, status);
        }

        private void HistoryUnitPeripheriesUpdate(IHistoryActionRepository historyActionRepository, IHistoryActionEditRepository historyActionEditRepository, int unitId, string unitName, List<HistoryEditField> historyPeripheries, User user)
        {
            var item = new HistoryAction()
            {
                Action = (byte)HistoryActionType.EditPeripheries,
                Date = DateTime.Now.ToUniversalTime(),
                FormId = (byte)FormId.Unit,
                RecordId = unitId,
                RecordName = unitName,
                UserId = user.Id
            };
            int newId = historyActionRepository.Add(item);

            var list = new List<HistoryActionEdit>();
            foreach (var peripheryItem in historyPeripheries)
            {
                var field = new HistoryActionEdit()
                {
                    FieldId = peripheryItem.fieldId,
                    FieldType = peripheryItem.fieldType,
                    FieldValueOld = peripheryItem.oldValue,
                    FieldValueActual = peripheryItem.newValue,
                    HistoryActionId = newId
                };
                list.Add(field);
            }
            historyActionEditRepository.AddList(list);
        }

        public Response<UnitDetail?> ServicePeripheries(IConfiguration configuration, IMessageLogger logger, IUnitRepository unitRepository, IPubRepository pubRepository, IPeripheryRepository peripheryRepository, IHistoryActionRepository historyActionRepository, IHistoryActionEditRepository historyActionEditRepository, string handlePub, string deviceId, List<byte>? peripheries, string sessionId)
        {
            var unitReturn = (UnitDetail?)null;
            var status = ResponseStatus.OK;
            try
            {
                var sessionManager = new SessionManager();
                if (sessionManager.Exists(sessionId, out User? user) && user != null)
                {
                    var pub = pubRepository.GetByHandle(handlePub);
                    if (pub != null)
                    {
                        var unit = unitRepository.GetByDeviceId(deviceId);
                        if (unit != null)
                        {
                            if (peripheries != null && peripheries.Any())
                            {
                                List<byte> peripheriesToCheck = new();
                                Dictionary<byte, (byte, string)> peripheryInfo = new();
                                foreach (var p in peripheries)
                                {
                                    byte internalPeriphery = PeripheryConvertor.FromExternal(p);
                                    if (!peripheryInfo.ContainsKey(internalPeriphery))
                                        peripheryInfo.Add(internalPeriphery, (p, PeripheryConvertor.GetPeripheryNameByExternalType(p)));
                                    peripheriesToCheck.Add(internalPeriphery);
                                }

                                //if there is any periphery to add
                                var peripheriesToAdd = CheckPeripheries(peripheryRepository, peripheriesToCheck, unit);
                                if (peripheriesToAdd.Any())
                                {
                                    var addedPeriphery = new List<Periphery>();
                                    var historyTable = new List<HistoryEditField>();
                                    foreach (var pid in peripheriesToAdd)
                                    {
                                        var peripheryName = peripheryInfo[pid].Item2;
                                        var item = peripheryRepository.GetPeripheryItem(unit.Id, pid, null, null, peripheryName);
                                        addedPeriphery.Add(item);
                                        historyTable.Add(new HistoryEditField(item.Type, (int)FieldType.Integer, String.Empty, peripheryName));
                                    }
                                    peripheryRepository.AddRange(addedPeriphery);
                                    HistoryHelper.HistoryUnitPeripheriesUpdate(unit.Id, deviceId, historyTable, user, historyActionEditRepository, historyActionRepository);
                                }
                            }
                        }
                        else
                        {
                            status = ResponseStatus.ErrorUnitNotFound;
                        }
                    }
                    else
                    {
                        status = ResponseStatus.ErrorPubNotFound;
                    }
                }
                else
                    status = ResponseStatus.ErrorUserNotLogged;
            }
            catch (Exception ex)
            {
                logger.Log(configuration, ex);
                status = ResponseStatus.ErrorUnknown;
            }
            return new Response<UnitDetail?>(unitReturn, status);
        }

        private List<Periphery> GetPeripheriesItems(IPeripheryRepository peripheryRepository, List<PeripheryAddItem>? peripheries, Unit unit)
        {
            var allPeripheries = new List<Periphery>();
            List<string> serials = peripheries?.Where(p => !String.IsNullOrEmpty(p.SerialNumber))?.Select(p => p?.SerialNumber?.ToString() ?? string.Empty).ToList() ?? new List<string>();

            if (serials.Any())
            {
                var per = peripheryRepository.GetUnitPeripheriesBySerialNumbers(unit.Id, serials);
                if (per != null && per.Any())
                    allPeripheries.AddRange(per);
            }

            List<byte> types = peripheries?.Where(p => String.IsNullOrEmpty(p.SerialNumber))?.Select(p => p.Type).ToList() ?? new List<byte>();
            if (types.Any())
            {
                var per = peripheryRepository.GetUnitPeripheriesByType(unit.Id, types);
                if (per != null && per.Any())
                    allPeripheries.AddRange(per);
            }

            return allPeripheries;
        }

        private List<byte> CheckPeripheries(IPeripheryRepository peripheryRepository, List<byte> peripheryTypes, Unit unit)
        {
            var peripheriesList = peripheryTypes.GroupBy(p => p).ToDictionary(n => n.Key, n => n.Count());

            var toAdd = peripheryRepository.GetUnitPeripheriesByType(unit.Id, peripheriesList);

            return toAdd;
        }

        private UnitDetail GetUnitDetail(IPeripheryRepository peripheryRepository, Unit unitDB)
        {
            var peripheriesDB = peripheryRepository.GetUnitPeripheriesWithOrders(unitDB.Id);
            var unit = new UnitDetail()
            {
                Id = unitDB.Id,
                Name = unitDB.Name,
                DeviceId = unitDB.DeviceId,
                PubId = unitDB.PubId,
                HasUnit = unitDB.HasSimcard,
                Peripheries = new List<PeripheryItem>()
            };

            if (peripheriesDB != null && peripheriesDB.Any())
            {
                unit.Peripheries = peripheriesDB.Select(item => new PeripheryItem()
                {
                    Id = item.Item1.Id,
                    Type = PeripheryConvertor.ToExternal(item.Item1.Type),
                    SerialNumber = item.Item1.SerialNumber,
                    PressureSensorId = item.Item1.PressureSensorId,
                    Order = item.Item2
                }).ToList();
            }
            return unit;
        }



        public Response<WifiListResponse?> GetWifiList(IConfiguration configuration, IMessageLogger logger, IUnitRepository unitRepository, IUnitWifiRepository unitWifiRepository, string deviceId, string sessionId)
        {
            WifiListResponse? response = null;
            var status = ResponseStatus.ErrorUnknown;

            try
            {
                var sessionManager = new SessionManager();

                if (sessionManager.Exists(sessionId, out User? user) && user != null)
                {
                    var unit = unitRepository.GetByDeviceId(deviceId);
                    if (unit == null)
                        status = ResponseStatus.ErrorUnitNotFound;
                    else
                    {
                        var wifiList = unitWifiRepository.GetByDeviceId(deviceId);
                        response = new WifiListResponse()
                        {
                            Data = wifiList.Select(o => new Wifi()
                            {
                                Date = o.Date,
                                SSID = o.SSID,
                                Signal = o.Signal
                            }).ToList()
                        };

                        status = ResponseStatus.OK;
                    }
                }
            }
            catch (Exception ex)
            {
                logger.Log(configuration, ex);
                status = ResponseStatus.ErrorUnknown;
            }
            return new Response<WifiListResponse?>(response, status);
        }

        private void SetTaskOnIODApi(string taskHandle, string userToken, string deviceId, int userId, byte state, ICommunicationIODAPIerrorRepository communicationIODAPIerrorRepository, string raAddress)
        {
            try
            {
                using (var client = new HttpClient())
                {
                    client.DefaultRequestHeaders.Add("Authorization", $"bearer {userToken}");
                    var jsonContent = "{\"serialNumber\":\"" + deviceId + "\"}";
                    var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");
                    var response = client.PostAsync($"{raAddress}task/{taskHandle}/unit", content).Result;

                    if (!response.IsSuccessStatusCode || (int)response.StatusCode != (int)ResponseStatus.ResponseCreated)
                    {
                        throw new Exception($"Failed to send task information. Response Status Code: {response.StatusCode}, Response success: {response.IsSuccessStatusCode}");
                    }
                }
            }
            catch (Exception ex)
            {
                SetCommunicationIODAPIerror(taskHandle, userId, deviceId, state, ex.Message, communicationIODAPIerrorRepository);
            }
        }

        private void SetTaskStatus(string taskHandle, string userToken, string status, string deviceId, int userId, byte state, ICommunicationIODAPIerrorRepository communicationIODAPIerrorRepository, string raAddress)
        {
            try
            {
                using (var client = new HttpClient())
                {
                    client.DefaultRequestHeaders.Add("Authorization", $"bearer {userToken}");
                    var jsonContent = "{\"status\":\"" + status + "\"}";
                    var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");
                    var response = client.PostAsync($"{raAddress}tasks/{taskHandle}/status", content).Result;

                    if (!response.IsSuccessStatusCode || (int)response.StatusCode != (int)ResponseStatus.ResponseCreated)
                    {
                        throw new Exception("Failed to send task information");
                    }
                }
            }
            catch (Exception ex)
            {
                SetCommunicationIODAPIerror(taskHandle, userId, deviceId, state, ex.Message, communicationIODAPIerrorRepository);
            }
        }

        private void SetCommunicationIODAPIerror(string taskHandle, int userId, string deviceId, byte state, string errorMessage, ICommunicationIODAPIerrorRepository communicationIODAPIerrorRepository) => communicationIODAPIerrorRepository.AddError(DateTime.Now, taskHandle, userId, deviceId, state, errorMessage);


    }
}