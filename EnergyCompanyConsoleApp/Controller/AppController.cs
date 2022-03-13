
using EnergyCompanyConsoleApp.Dados;
using EnergyCompanyConsoleApp.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnergyCompanyConsoleApp.Controller
{
    public class AppController : DataBase
    {
        public Endpoint SelectedEndpoint;
        public void NewEndpoin(out string error, List<string> form)
        {
            try
            {
                if (EndpointIsValid(form, out error))
                {
                    SaveEnpont(ParseEndpoing(form));
                }

            }
            catch (Exception ex)
            {
                error = ex.Message;
            }   
   
        }

        private bool EndpointIsValid(List<string> form, out string erro)
        {
            erro = "";

            if (FindEndpointbySerial(form[0]) != null)
            {
                erro = "Serial Number already exist";
                return false;
            }
            if (!form[2].All(char.IsDigit))
            {
                erro = "Meter Number is invalid";
                return false;
            }
          
            return true;
        }

        public Endpoint ParseEndpoing(List<string> form)
        {
            Endpoint endpoint = new Endpoint()
            {
                SerialNumber= form[0],
                MeterModelId = int.Parse(form[1]),
                MeterNumber = int.Parse(form[2]),
                MeterFirmwareVersion = form[3],
                SwitchState = int.Parse(form[4]),
            };

            return endpoint;
        }

        public List<Endpoint> ListAll()
        {
            return GetAll();
        }

        public void FindEndpoint(string SerialNumber, out string erro)
        {
            erro = "";

            try
            {
                SelectedEndpoint = FindEndpointbySerial(SerialNumber);

                if (SelectedEndpoint == null)
                {
                    erro = "THE ENDPOINT WAS NOT FOUND!!";
                }

            }
            catch (Exception ex)
            {
                erro = ex.Message;
            }
  
        }

        public void DeleteEndPoint()
        {
            RemoveEnpont(SelectedEndpoint);
        }

        public void EditEndPoint(string state)
        {
            AlterSwitchState(SelectedEndpoint.SerialNumber, int.Parse(state));
        }


    }
}
