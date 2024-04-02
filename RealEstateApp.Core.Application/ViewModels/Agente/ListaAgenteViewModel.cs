using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealEstateApp.Core.Application.ViewModels.Agente
{
    public class ListaAgenteViewModel
    {
        public List<AgenteViewModel> Agentes { get; set; }
        public string SearchString { get; set; }
    }
}
