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

        Task<ApiResult<Version>> CheckNewVersion(string versionNumber, string platform);

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
                    Platform temp;

                    if (Enum.TryParse<Platform>(request.Platform, out temp))
                    {
                       
                        var version = new Version
                        {
                            Content = request.Content,
                            isRequired = request.isRequired,
                            VersionNumber = request.VersionNumber,
                            Platform = temp,
                            ReleaseTime = request.ReleaseTime,
                            Status = request.Status,
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

                    return new ApiErrorResult<string>(404, "PlatformNotFound");
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

        //Lấy thông tin các phiên bản (có thể lọc thông qua nền tảng platform)
        public async Task<ApiResult<List<VersionVM>>> GetVerionPlatform(string platform)
        {
            try
            {
                //Lấy toàn bộ
                if(platform == null)
                {
                    var res = await _context.Version.Select(x => new VersionVM()
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
                        return new ApiSuccessResult<List<VersionVM>>("GetAllVersionSuccessful", res);

                    }
                    return new ApiErrorResult<List<VersionVM>>(400, "GetAllVersionPlatformFail");
                }

                //Lấy theo nền tảng
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

        //Kiểm tra phiên bản mới (tham số: phiên bản hiện tại và nền tảng)
         public async Task<ApiResult<Version>> CheckNewVersion(string versionNumber, string platform)
        {
            try
            {
                Platform temp;

                if (Enum.TryParse<Platform>(platform, out temp))
                {
                    //Lấy thông tin phiên bản hiện tại
                    var curr_version = await _context.Version.Where(x => x.Platform.Equals(temp) && x.VersionNumber == versionNumber).SingleOrDefaultAsync();

                    if(curr_version==null)
                    {
                        return new ApiErrorResult<Version>(404, "VersionNotFound");
                    }

                    //Lấy thông tin phiên bản mới nhất
                    var last_version = await _context.Version
                        .Where(x => x.Platform.Equals(temp))
                        .OrderByDescending(x => x.ReleaseTime)
                        .FirstOrDefaultAsync();

                    //Lấy thông tin phiên bản bắt buộc phải cập nhật
                    var version_required = await _context.Version
                        .Where(x => x.Platform.Equals(temp) && x.isRequired == true && x.ReleaseTime > curr_version.ReleaseTime)
                        .OrderByDescending(x => x.ReleaseTime)
                        .FirstOrDefaultAsync();

                    //Trường hợp phiên bản hiện tại cũng là phiên bản mới nhất
                    if (curr_version.VersionNumber == last_version.VersionNumber)
                    {
                        return new ApiSuccessResult<Version>(200, "AlreadyTheLatestVersion", curr_version);
                    }
                    //Trường hợp có phiên bản mới hơn và bắt buộc phải cập nhật
                    else if (version_required != null && version_required.ReleaseTime > curr_version.ReleaseTime)
                    {
                        return new ApiSuccessResult<Version>(201, "MustHaveUpdateVersion", version_required);
                    }
                    //Trường hợp chỉ có phiên bản mới (có thể không yêu cầu cập nhật)
                    else
                        return new ApiSuccessResult<Version>(202, "HaveUpdateVersion", last_version);

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

