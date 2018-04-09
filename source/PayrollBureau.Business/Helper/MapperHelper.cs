using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PayrollBureau.Business.Models;
using PayrollBureau.Data.Extensions;
using PayrollBureau.Data.Models;

namespace PayrollBureau.Business.Helper
{
    public static class MapperHelper
    {
        public static Bureau FromBureau(Data.Entities.Bureau bureau)
        {
            var bureauResult = new Models.Bureau
            {
                BureauId = bureau.BureauId,
                CreatedBy = bureau.CreatedBy,
                CreatedDateUtc = bureau.CreatedDateUtc,
                Name = bureau.Name
            };
            return bureauResult;
        }


        public static PagedResult<Bureau> FromBureauPagedResult(IEnumerable<Data.Entities.Bureau> bureaus, Paging paging)
        {
            return bureaus.Select(e => new Bureau
            {
                BureauId = e.BureauId,
                CreatedBy = e.CreatedBy,
                CreatedDateUtc = e.CreatedDateUtc,
                Name = e.Name
            }).ToList().AsQueryable().Paginate(paging);
        }

    }
}
