
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

        /// <summary>
        /// Create an new endpoint
        /// </summary>
        /// <param name="error">gives it back an error, if there is.</param>
        /// <param name="form">Submit the form to be created.</param>
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

        /// <summary>
        /// Checks if the form is valid.
        /// </summary>
        /// <param name="form">form to be created</param>
        /// <param name="erro">gives it back an error, if there is.</param>
        /// <returns></returns>
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

        /// <summary>
        /// Parse List String to List Endpoint
        /// </summary>
        /// <param name="form">form to be parse</param>
        /// <returns></returns>
        public Endpoint ParseEndpoing(List<string> form)
        {
            Endpoint endpoint = new Endpoint()
            {
                SerialNumber = form[0],
                MeterModelId = int.Parse(form[1]),
                MeterNumber = int.Parse(form[2]),
                MeterFirmwareVersion = form[3],
                SwitchState = int.Parse(form[4]),
            };

            return endpoint;
        }


        /// <summary>
        /// Return All Endpoints Saved.
        /// </summary>
        /// <returns></returns>
        public List<Endpoint> ListAll()
        {
            return GetAll();
        }


        /// <summary>
        /// Find Endpoint by Serial Number: Return an Endpoint saved or a error if not found.
        /// </summary>
        /// <param name="SerialNumber">Serial Number to find</param>
        /// <param name="erro">gives it back an error, if there is.</param>
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

        /// <summary>
        /// Remove an endpoint saved
        /// </summary>
        public void DeleteEndPoint()
        {
            RemoveEnpont(SelectedEndpoint);
        }

        /// <summary>
        /// Edite the switch state of a endpoint.
        /// </summary>
        /// <param name="state">New state</param>
        public void EditEndPoint(string state)
        {
            AlterSwitchState(SelectedEndpoint.SerialNumber, int.Parse(state));
        }


    }
}
