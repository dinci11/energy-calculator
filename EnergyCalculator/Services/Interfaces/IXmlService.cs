using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnergyCalculator.Services.Interfaces
{
    public interface IXmlService
    {
        Task LoadFileAsync();
    }
}