using System.Threading.Tasks;

namespace AOC
{
    public interface IDay
    {
        public Task<Results> Run(string? input = null);
    }
}
