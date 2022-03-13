using EnergyCompanyConsoleApp.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnergyCompanyConsoleApp.Dados
{
    public class DataBase 
    {
        private List<Endpoint> EndPoints;

        public DataBase()
        {
            EndPoints = new List<Endpoint>();
        }

        protected void SaveEnpont(Endpoint endpoint) 
        { 
            EndPoints.Add(endpoint);
        }

        protected void RemoveEnpont(Endpoint endpoint)
        {
            EndPoints.Remove(endpoint);
        }

        protected List<Endpoint> GetAll()
        {
            return EndPoints;
        }

        protected Endpoint FindEndpointbySerial(string arg)
        {
            return EndPoints.Where(x => x.SerialNumber.ToLower() == arg.ToLower()).FirstOrDefault();   
        }


        protected void AlterSwitchState(string arg, int newState)
        {
            EndPoints.Where(x => x.SerialNumber.ToLower() == arg.ToLower()).FirstOrDefault().SwitchState = newState;
        }

    }
}
