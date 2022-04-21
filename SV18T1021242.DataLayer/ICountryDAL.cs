using SV18T1021242.DomainModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SV18T1021242.DataLayer
{
    public interface ICountryDAL
    {
        IList<Country> List();
    }
}
