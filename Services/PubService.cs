namespace SmartSense.API.ServiceModule.Services
{
    public interface IPubService
    {
        Response<PubList?> GetPubList(IConfiguration configuration, IMessageLogger logger, IPubApiInfoRepository pubApiInfoRepository, string sessionId);
        Response<PubDetail?> GetPubDetail(IConfiguration configuration, IMessageLogger logger, IPubRepository pubRepository, IPubRequestRepository pubRequestRepository, string supportPhoneNumber, int pubId, string sessionId);
    }

    public class PubService : IPubService
    {
        public Response<PubList?> GetPubList(IConfiguration configuration, IMessageLogger logger, IPubApiInfoRepository pubApiInfoRepository, string sessionId)
        {
            var status = ResponseStatus.ErrorUnknown;
            PubList? pubList = null;

            try
            {
                var sessionManager = new SessionManager();

                if (sessionManager.Exists(sessionId, out User? user) &&
                    user != null)
                {
                    pubList = new PubList();
                    pubList.Pubs = new List<PubItem>();
                    pubList.Pubs = pubApiInfoRepository.GetPubs()?.Select(p => new PubItem()
                    {
                        Id = p.Id,
                        Name = p.Name,
                        Address = p.Address,
                        Contact = p.PhoneNumber,
                        MontageRequest = p.MontageRequest
                    }).ToList();
                    status = ResponseStatus.OK;
                }
                else
                    status = ResponseStatus.ErrorUserNotLogged;
            }
            catch (Exception ex)
            {
                logger.Log(configuration, ex);
                status = ResponseStatus.ErrorUnknown;
            }
            return new Response<PubList?>(pubList, status);
        }

        public Response<PubDetail?> GetPubDetail(IConfiguration configuration, IMessageLogger logger, IPubRepository pubRepository, IPubRequestRepository pubRequestRepository, string supportPhoneNumber, int pubId, string sessionId)
        {
            var status = ResponseStatus.ErrorUnknown;
            PubDetail? pubDetail = null;

            try
            {
                var sessionManager = new SessionManager();

                if (sessionManager.Exists(sessionId, out User? user) &&
                    user != null)
                {
                    var pub = pubRepository.GetPub(pubId);
                    bool montageRequest = pubRequestRepository.GetPubRequests(pubId, (byte)ServiceType.Montage, true)?.Any() ?? false;
                    if (pub != null)
                    {
                        pubDetail = new PubDetail()
                        {
                            Id = pubId,
                            Name = pub.Name,
                            Address = pub.Address,
                            PhoneNumber = pub.PhoneNumber,
                            MontageRequest = montageRequest,
                            SupportPhoneNumber = supportPhoneNumber
                        };
                        status = ResponseStatus.OK;
                    }
                    else
                        status = ResponseStatus.ErrorPubNotFound;
                }
                else
                    status = ResponseStatus.ErrorUserNotLogged;
            }
            catch (Exception ex)
            {
                logger.Log(configuration, ex);
                status = ResponseStatus.ErrorUnknown;
            }
            return new Response<PubDetail?>(pubDetail, status);
        }
    }
}
