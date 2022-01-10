using System.Threading.Tasks;
using FakeNewsFilter.Data.EF;
using FakeNewsFilter.Data.Entities;
namespace FakeNewsFilter.Application.Common
{
    public class SourceCommon
    {
        public static async Task<Source> CheckExistSource(ApplicationDBContext _context, int sourceId)
        {
            var source = await _context.Source.FindAsync(sourceId);
            return source;
        }

        public static async Task<Source> CheckExistSourceName(ApplicationDBContext _context, string sourceName)
        {
            var sourceNameUpdate = await _context.Source.FindAsync(sourceName);
            return sourceNameUpdate;
        }
    }
}
