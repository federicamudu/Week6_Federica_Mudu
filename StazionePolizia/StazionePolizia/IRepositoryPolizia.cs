using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StazionePolizia
{
    internal interface IRepositoryPolizia
    {
        void GetAllAgents();
        void InserisciAgente(Agente agente);
        void AgentiArea(string area);
        void AgentiConAnniDiServizio(int anni);
        bool VerificaInserimentoAgenteConCF(string cf);
        bool VerificaPresenzaAgenteArea(string area);
    }
}
