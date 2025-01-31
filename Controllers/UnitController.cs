using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json.Linq;
using System.ComponentModel;

namespace SmartSense.API.ServiceModule.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UnitController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly IMessageLogger _logger;
        private readonly DataContext _context;
        private readonly IUnitService _unitService;
        private readonly IUnitRepository unitRepository;
        private readonly IDeviceSettingsChangeRepository deviceSettingsChangeRepository;
        private readonly IPeripheryRepository peripheryRepository;
        private readonly IHubRepository hubRepository;
        private readonly IServiceRepository serviceRepository;
        private readonly IPeripheryLocalizationRepository peripheryLocalizationRepository;
        private readonly IUserPermissionRepository userPermissionRepository;
        private readonly ITapUnitOrderRepository tapUnitOrderRepository;
        private readonly IUnitPeripheriesDataFluidRepository unitPeripheriesDataFluidRepository;
        private readonly IUnitServicePictureRepository unitServicePictureRepository;
        private readonly IServiceComponentRepository serviceComponentRepository;
        private readonly IAlertMessageInfoRepository alertMessageInfoRepository;
        private readonly IUnitWifiRepository unitWifiRepository;
        private readonly IPubApiInfoRepository pubApiInfoRepository;
        private readonly IComponentPriceRepository componentPriceRepository;
        private readonly IUnitEventRepository unitEventRepository;
        private readonly PubRepository pubRepository;
        private readonly IHistoryActionRepository historyActionRepository;
        private readonly IHistoryActionEditRepository historyActionEditRepository;
        private readonly ICommunicationIODAPIerrorRepository communicationIODAPIerrorRepository;

        public UnitController(IConfiguration configuration, IMessageLogger logger,
            DataContext context, IUnitService unitService)
        {
            _configuration = configuration;
            _logger = logger;
            _context = context;
            _unitService = unitService;

            unitRepository = new UnitRepository(_context);
            peripheryRepository = new PeripheryRepository(_context);
            hubRepository = new HubRepository(_context);
            deviceSettingsChangeRepository = new DeviceSettingsChangeRepository(_context);
            serviceRepository = new ServiceRepository(_context);
            peripheryLocalizationRepository = new PeripheryLocalizationRepository(_context);
            userPermissionRepository = new UserPermissionRepository(_context);
            tapUnitOrderRepository = new TapUnitOrderRepository(_context);
            unitPeripheriesDataFluidRepository = new UnitPeripheriesDataFluidRepository(_context);
            unitServicePictureRepository = new UnitServicePictureRepository(_context);
            serviceComponentRepository = new ServiceComponentRepository(_context);
            alertMessageInfoRepository = new AlertMessageInfoRepository(_context);
            unitWifiRepository = new UnitWifiRepository(_context);
            pubApiInfoRepository = new PubApiInfoRepository(_context);
            componentPriceRepository = new ComponentPriceRepository(_context);
            unitEventRepository = new UnitEventRepository(_context);
            pubRepository = new PubRepository(_context);
            historyActionRepository = new HistoryActionRepository(_context);
            historyActionEditRepository = new HistoryActionEditRepository(_context);
            communicationIODAPIerrorRepository = new CommunicationIODAPIerrorRepository(_context);
        }

        // Not used becausethere could be more units with the same set number
        /// <summary>
        /// Return detail of unit based on sent SetCode
        /// </summary>
        /// <param name="request">Set sessionId (from login) and SetCode of unit</param>
        /// <returns></returns>
        //[HttpPost("[action]"), ActionName("GetUnit")]
        //public Response<UnitDetail?> GetUnit([FromBody] UnitDetailRequest request)
        //{
        //    return _unitService.GetUnit(_configuration, _logger, unitRepository, peripheryRepository, request.SetCode, request.SessionId);
        //}

        /// <summary>
        /// Add new unit according to requested data and default values
        /// </summary>
        /// <param name="request">Set sessionId (from login), pub handle, deviceId, SetCode, NFC and peripheries</param>
        /// <returns></returns>
        [HttpPost("[action]"), ActionName("SetUnit")]
        public Response<UnitDetail?> SetUnit([FromBody] SetUnitRequest request)
        {
            return _unitService.SetUnit(_configuration, _logger, unitRepository, peripheryRepository, pubRepository, historyActionRepository, historyActionEditRepository, request.HandlePub, request.DeviceId, request.SetCode, request.NFC, request.HasSimcard, request.Peripheries, request.SessionId);
        }

        /// <summary>
        /// Decrease unit data interval for unit
        /// </summary>
        /// <param name="request">Set sessionId (from login) and DeviceId of unit</param>
        /// <returns></returns>
        [HttpPost("[action]"), ActionName("SetUnitDataIntervalLow")]
        public Response<string> SetUnitLowInterval([FromBody] SetUnitLowIntervalRequest request)
        {
            int interval = Config.UnitNormalIntervalValue(_configuration);
            var data = new UnitSettings()
            {
                IntervalChanged = true,
                Interval = interval
            };

            var unitSettingsSerializer = UnitSettingsProvider.GetSerializer();

            return _unitService.SetUnitLowInterval(_configuration, _logger, unitRepository, deviceSettingsChangeRepository, userPermissionRepository, request.DeviceId, unitSettingsSerializer.Serialize(data), interval, request.SessionId);
        }

        /// <summary>
        /// Start service with type montage or service IOD (depending on unit´s state) on requested Unit
        /// </summary>
        /// <param name="request">Set sessionId (from login) and DeviceId of unit</param>
        /// <returns></returns>
        [HttpPost("[action]"), ActionName("StartMontage")]
        public Response<string> StartMontage([FromBody] StartServiceRequest request) => _unitService.StartMontage(_configuration, _logger, _context, unitRepository, serviceRepository, userPermissionRepository, communicationIODAPIerrorRepository, request.DeviceId, Config.RestApiAddress(_configuration), request.TaskHandle, request.UserToken, request.SessionId);

        /// <summary>
        /// Set montage service as successfully finished and set unit to active
        /// </summary>
        /// <param name="request">Set sessionId (from login) and DeviceId of unit</param>
        /// <returns></returns>
        [HttpPost("[action]"), ActionName("FinishMontage")]
        public Response<string> FinishMontage([FromBody] FinishMontageRequest request)
        {
            int interval = Config.UnitNormalIntervalValue(_configuration);
            var data = new UnitSettings()
            {
                IntervalChanged = true,
                Interval = interval
            };

            var unitSettingsSerializer = UnitSettingsProvider.GetSerializer();
            return _unitService.SuccessMontage(_configuration, _logger, _context, unitRepository, serviceRepository, deviceSettingsChangeRepository, userPermissionRepository, communicationIODAPIerrorRepository, request.DeviceId, unitSettingsSerializer.Serialize(data), interval, request.SessionId, Config.RestApiAddress(_configuration), request.TaskHandle, request.UserToken);
        }

        /// <summary>
        /// Set montage service as unsuccessfully finished and set unit as new
        /// </summary>
        /// <param name="request">Set sessionId (from login) and DeviceId of unit</param>
        /// <returns></returns>
        [HttpPost("[action]"), ActionName("CancelMontage")]
        public Response<string> CancelMontage([FromBody] FinishMontageRequest request)
        {
            int interval = Config.UnitNormalIntervalValue(_configuration);
            var data = new UnitSettings()
            {
                IntervalChanged = true,
                Interval = interval
            };

            var unitSettingsSerializer = UnitSettingsProvider.GetSerializer();

            return _unitService.CancelMontage(_configuration, _logger, _context, unitRepository, serviceRepository, deviceSettingsChangeRepository, userPermissionRepository, request.DeviceId, unitSettingsSerializer.Serialize(data), interval, request.SessionId);
        }

        /// <summary>
        /// Set unit wifi settings
        /// </summary>
        /// <param name="request">Set sessionId (from login), DeviceId of unit and wifi settings</param>
        /// <returns></returns>
        [HttpPost("[action]"), ActionName("SetWifiSettings")]
        public Response<string> SetWifiSettings([FromBody] SetWifiSettingsRequest request)
        {
            var data = new UnitSettings()
            {
                WifiChanged = request.Wifi.HasValue,
                Wifi = request.Wifi ?? false,
                SSIDChanged = request.WifiSSID != null,
                SSID = request.WifiSSID,
                PasswordChanged = request.Password != null,
                Password = request.Password,
            };

            var unitSettingsSerializer = UnitSettingsProvider.GetSerializer();

            return _unitService.SetSettings(_configuration, _logger, unitRepository, deviceSettingsChangeRepository, userPermissionRepository, request.DeviceId, unitSettingsSerializer.Serialize(data), wifi: request.Wifi, SSID: request.WifiSSID, password: request.Password, request.SessionId);
        }

        /// <summary>
        /// Show Periphery localization (SN, Slot, Hub SN and Hub slot) for required periphery
        /// </summary>
        /// <param name="request">Set sessionId (from login) and periphery id</param>
        /// <returns></returns>
        [HttpPost("[action]"), ActionName("GetPeripheryLocalization")]
        public Response<PeripheryLocalizationInfo?> GetPeripheryLocalization([FromBody] GetPeripheryLocalizationRequest request)
        {
            return _unitService.GetPeripheryLocalization(_configuration, _logger, peripheryLocalizationRepository, request.PeripheryId, request.SessionId);
        }

        /// <summary>
        /// Set order of requested tap and set name of tap according to the order (order + 1)
        /// </summary>
        /// <param name="request">Set sessionId (from login), DeviceId of unit, periphery id (for requested tap) and requested order.</param>
        /// <returns></returns>
        [HttpPost("[action]"), ActionName("SetTapUnitOrder")]
        public Response<string> SetTapUnitOrder([FromBody] SetTapUnitOrderRequest request)
        {
            return _unitService.SetTapUnitOrder(_configuration, _logger, tapUnitOrderRepository, userPermissionRepository, peripheryRepository, request.PeripheryId, request.UnitId, request.Order, request.SessionId);
        }

        /// <summary>
        /// Return dictionary of fluid amount on peripheries on requested unit
        /// </summary>
        /// <param name="request">Set sessionId (from login), unit id and date from when you requested data</param>
        /// <returns></returns>
        [HttpPost("[action]"), ActionName("GetUnitPeripheriesData")]
        public Response<Dictionary<int, decimal>?> GetUnitPeripheriesData([FromBody] GetUnitPeripheriesDataRequest request)
        {
            return _unitService.GetUnitPeripheriesData(_configuration, _logger, unitPeripheriesDataFluidRepository, request.UnitId, request.From, request.SessionId);
        }

        /// <summary>
        /// Set state of unit to montage and start service of type service
        /// </summary>
        /// <param name="request">Set sessionId (from login) and DeviceId of unit</param>
        /// <returns></returns>
        [HttpPost("[action]"), ActionName("StartService")]
        public Response<int> StartService([FromBody] ServiceRequest request) => _unitService.StartService(_configuration, _logger, _context, unitRepository, serviceRepository, userPermissionRepository, request.DeviceId, request.SessionId);

        /// <summary>
        /// Successfully finish the service on requested unit and change unit´s state to active
        /// </summary>
        /// <param name="request">Set sessionId (from login) and DeviceId of unit</param>
        /// <returns></returns>
        [HttpPost("[action]"), ActionName("FinishService")]
        public Response<string> FinishService([FromBody] ServiceRequest request) => _unitService.SuccessService(_configuration, _logger, _context, unitRepository, serviceRepository, userPermissionRepository, communicationIODAPIerrorRepository, request.DeviceId, request.SessionId, Config.RestApiAddress(_configuration), request.TaskHandle, request.UserToken);

        /// <summary>
        /// Unsuccessfully finish the service on requested unit and change unit´s state to active
        /// </summary>
        /// <param name="request">Set sessionId (from login) and DeviceId of unit</param>
        /// <returns></returns>
        [HttpPost("[action]"), ActionName("CancelService")]
        public Response<string> CancelService([FromBody] ServiceRequest request) => _unitService.CancelService(_configuration, _logger, _context, unitRepository, serviceRepository, userPermissionRepository, request.DeviceId, request.SessionId);

        /// <summary>
        /// Add a note to service (type service) on requested device
        /// </summary>
        /// <param name="request">Set sessionId (from login) and DeviceId of unit and note</param>
        /// <returns></returns>
        [HttpPost("[action]"), ActionName("ServiceAddNote")]
        public Response<string> ServiceAddNote([FromBody] ServiceNoteRequest request) => _unitService.SetServiceNote(_configuration, _logger, unitRepository, serviceRepository, userPermissionRepository, request.DeviceId, request.Note, request.SessionId);

        /// <summary>
        /// Add picture to requested service
        /// </summary>
        /// <param name="request"></param>
        /// <returns>Set sessionId (from login), serviceId, component type and picture type</returns>
        [HttpPost("[action]"), ActionName("AddPicture")]
        public Response<string> AddServicePicture([FromBody] AddPictureRequest request) => _unitService.AddPicture(_configuration, _logger, userPermissionRepository, unitServicePictureRepository, request.ServiceId, request.Component, request.PictureType, request.FileData, request.FileName, request.SessionId);

        /// <summary>
        /// Get unit detail by NFC code
        /// </summary>
        /// <param name="request"></param>
        /// <returns>Set sessionId (from login) and NFC code</returns>
        [HttpPost("[action]"), ActionName("GetUnitByNFC")]
        public Response<UnitDetail?> GetUnitByNFC([FromBody] UnitByNFCRequest request) => _unitService.GetUnitByNFC(_configuration, _logger, unitRepository, peripheryRepository, request.NFC, request.SessionId);

        /// <summary>
        /// Return log for requested unit
        /// </summary>
        /// <param name="request">Set device id and date from when you requested data</param>
        /// <returns></returns>
        [HttpPost("[action]"), ActionName("LogIOD")]
        public Response<List<string>?> LogIOD([FromBody] CheckIODRequest request) =>
            _unitService.LogIOD(_configuration, _logger, userPermissionRepository, unitRepository, unitEventRepository, request.DeviceId, request.From, request.SessionId);

        /// <summary>
        /// Return list of peripheries for requested unit
        /// </summary>
        /// <param name="request">Set device id and date from when you requested data</param>
        /// <returns></returns>
        [HttpPost("[action]"), ActionName("CheckIOD")]
        public Response<CheckIODResponse?> CheckIOD([FromBody] CheckIODRequest request) =>
            _unitService.CheckIOD(_configuration, _logger, userPermissionRepository, unitRepository, peripheryRepository, hubRepository, request.DeviceId, request.From, request.SessionId);

        /// <summary>
        /// Set topology for unit
        /// </summary>
        /// <param name="request">Set device id and date from when you want to apply the changes</param>
        /// <returns></returns>
        [HttpPost("[action]"), ActionName("ConfirmIOD")]
        public Response<ConfirmIODResponse?> ConfirmIOD([FromBody] ConfirmIODRequest request) =>
            _unitService.ConfirmIOD(_configuration, _logger, userPermissionRepository, unitRepository, peripheryRepository, hubRepository, request.DeviceId, request.From, request.SessionId);

        /// <summary>
        /// Add service components for requested unit
        /// </summary>
        /// <param name="request">Set sessionId (from login), device id of unit and list of components you want to add </param>
        /// <returns></returns>
        [HttpPost("[action]"), ActionName("AddServiceComponents")]
        public Response<string> AddServiceComponents([FromBody] AddServiceComponentsRequest request) =>
            _unitService.AddServiceComponents(_configuration, _logger, serviceRepository, userPermissionRepository, serviceComponentRepository, tapUnitOrderRepository, unitRepository, pubApiInfoRepository, componentPriceRepository, request.UnitId, request.Components, request.SessionId);

        /// <summary>
        /// Get list of alerts for requested unit
        /// </summary>
        /// <param name="request">Set sessionId (from login) and unit id</param>
        /// <returns></returns>
        [HttpPost("[action]"), ActionName("GetAlertMessages")]
        public Response<AlertsList> GetAlertMessages([FromBody] UnitAlertMessagesRequest request) => _unitService.GetAllAlertMessages(_configuration, _logger, userPermissionRepository, peripheryRepository, alertMessageInfoRepository, unitRepository, request.UnitId, request.SessionId);

        /// <summary>
        /// Add service with type demontage and change unit state to Dismounted and move it to the pub !Sklad
        /// </summary>
        /// <param name="request">Set sessionId (from login) and unit id</param>
        /// <returns></returns>
        [HttpPost("[action]"), ActionName("DisassemblyUnit")]
        public Response<string> DisassemblyUnit([FromBody] DisassemblyUnitRequest request) => _unitService.DisassemblyUnit(_configuration, _logger, _context, userPermissionRepository, unitRepository, serviceRepository, pubRepository, historyActionRepository, historyActionEditRepository, request.UnitId, request.SessionId);

        /// <summary>
        /// Set periphery serial number
        /// </summary>
        /// <param name="request">Set sessionId (from login), periphery id and serial number to be set</param>
        /// <returns></returns>
        [HttpPost("[action]"), ActionName("ChangePeriphery")]
        public Response<string> ChangePeriphery([FromBody] ChangePeripheryRequest request) => _unitService.ChangePeriphery(_configuration, _logger, _context, userPermissionRepository, peripheryRepository, request.PeripheryId, request.SerialNumber, request.SessionId);

        /// <summary>
        /// Return list of available wifis for unit
        /// </summary>
        /// <param name="request">Set device id</param>
        /// <returns></returns>
        [HttpPost("[action]"), ActionName("GetWifiList")]
        public Response<WifiListResponse?> GetWifiList([FromBody] WifiListRequest request) =>
            _unitService.GetWifiList(_configuration, _logger, unitRepository, unitWifiRepository, request.DeviceId, request.SessionId);

        /// <summary>
        /// Check that unit has all set peripheries, if not add them
        /// </summary>
        /// <param name="request">Set sessionId (from login), pub handle, deviceId and peripheries (external types)</param>
        /// <returns></returns>
        [HttpPost("[action]"), ActionName("ServicePeripheries")]
        public Response<UnitDetail?> ServicePeripheries([FromBody] ServicePeripheriesRequest request)
        =>
            _unitService.ServicePeripheries(_configuration, _logger, unitRepository, pubRepository, peripheryRepository, historyActionRepository, historyActionEditRepository, request.HandlePub, request.DeviceId, request.Peripheries, request.SessionId);

        /// <summary>
        /// Remove all peripheries from set list (peripheries types in external types)
        /// </summary>
        /// <param name="request">Set sessionId (from login), pub handle, deviceId and peripheries (external types) to remove</param>
        /// <returns></returns>
        [HttpPost("[action]"), ActionName("RemovePeripheries")]
        public Response<UnitDetail?> RemovePeripheries([FromBody] RemovePeripheriesRequest request)
        =>
            _unitService.RemovePeripheries(_configuration, _logger, unitRepository, pubRepository, peripheryRepository, historyActionRepository, historyActionEditRepository, request.HandlePub, request.DeviceId, request.PeripheriesToRemove, request.SessionId);
            
    }
}