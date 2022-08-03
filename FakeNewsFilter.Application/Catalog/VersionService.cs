using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FakeNewsFilter.Data.EF;
using FakeNewsFilter.Data.Entities;
using FakeNewsFilter.Data.Enums;
using FakeNewsFilter.ViewModel.Catalog.Version;
using FakeNewsFilter.ViewModel.Common;
using Microsoft.EntityFrameworkCore;
using Version = FakeNewsFilter.Data.Entities.Version;

namespace FakeNewsFilter.Application.Catalog
{
    public interface IVersionService
    {
        Task<ApiResult<List<VersionVM>>> GetVerionPlatform(string platform);

        Task<ApiResult<string>> Create(VersionCreateRequest request);

        Task<ApiResult<VersionVM>> GetVersionDetail(int versionId);

        Task<ApiResult<Version>> CheckNewVersion(float versionNumber, string platform);

        Task<ApiResult<string>> Delete(int TopicId);
    }

    public class VersionService : IVersionService
    {
        private readonly ApplicationDBContext _context;


        public VersionService(ApplicationDBContext context)
        {
            _context = context;
        }

       
        public async Task<ApiResult<string>> Create(VersionCreateRequest request)
        {
            using (var transaction = _context.Database.BeginTransaction())
            {

                try
                {
                    var version = new Version
                    {
                        VersionNumber = request.VersionNumber,
                        Content = request.Content,
                        Platform = request.Platform,
                        ReleaseTime = request.ReleaseTime,
                        isRequired = request.isRequired,
                        Status = request.Status
                    };

                    _context.Version.Add(version);

                    var result = await _context.SaveChangesAsync();

                    if (result > 0)
                    {
                        transaction.Commit();
                        return new ApiSuccessResult<string>("CreateVersionSuccessful");
                    }

                    transaction.Rollback();
                    return new ApiErrorResult<string>(400, "CreateUnsuccessful", " " + result.ToString());
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    return new ApiErrorResult<string>(500, ex.Message);
                }
            }
        }

        public Task<ApiResult<string>> Delete(int TopicId)
        {
            throw new global::System.NotImplementedException();
        }

        public async Task<ApiResult<List<VersionVM>>> GetVerionPlatform(string platform)
        {
            try
            {
                Platform temp;

                if(Enum.TryParse<Platform>(platform, out temp))
                {
                    var res = await _context.Version.Where(x => x.Platform.Equals(temp))
                   .Select(x => new VersionVM()
                   {
                       VersionId = x.VersionId,
                       Content = x.Content,
                       isRequired = x.isRequired,
                       VersionNumber = x.VersionNumber,
                       CreateTime = x.CreateTime,
                       Platform = x.Platform.ToString(),
                       ReleaseTime = x.ReleaseTime,
                       Status = x.Status.ToString(),
                   }).ToListAsync();

                    if (res.Count > 0)
                    {
                        return new ApiSuccessResult<List<VersionVM>>("GetVersionPlatformSuccessful", res);

                    }
                    return new ApiErrorResult<List<VersionVM>>(400, "GetVersionPlatformFail");
                }
                else
                {
                    return new ApiErrorResult<List<VersionVM>>(404, "PlatformNotFound");
                }
                
            }
            catch(Exception ex)
            {
                return new ApiErrorResult<List<VersionVM>>(500, ex.Message);
            }
        }

         public async Task<ApiResult<Version>> CheckNewVersion(float versionNumber, string platform)
        {
            try
            {
                Platform temp;

                if (Enum.TryParse<Platform>(platform, out temp))
                {
                    var res = await _context.Version
                        .Where(x => x.Platform.Equals(temp))
                        .OrderByDescending(x => x.ReleaseTime)
                        .FirstOrDefaultAsync();
                       
                    if(res.VersionNumber > versionNumber)
                    {
                        return new ApiSuccessResult<Version>("HaveNewsVersion", res);
                    }
                    if(res.VersionNumber == versionNumber)
                        return new ApiSuccessResult<Version>("AlreadyTheLatestVersion");
                    else
                        return new ApiErrorResult<Version>(404, "VersionNotFound");
                }
                else
                {
                    return new ApiErrorResult<Version>(404, "PlatformNotFound");
                }

            }
            catch (Exception ex)
            {
                return new ApiErrorResult<Version>(500, ex.Message);
            }
        }
        public Task<ApiResult<VersionVM>> GetVersionDetail(int versionId)
        {
            throw new global::System.NotImplementedException();
        }
    }
}

