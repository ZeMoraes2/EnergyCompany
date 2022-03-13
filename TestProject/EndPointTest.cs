using EnergyCompanyConsoleApp.Controller;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace TestProject
{
    [TestClass]
    public class EndPointTest
    {
        [TestMethod]
        public void TestNewEndpoint()
        {
            List<string> form = new List<string>();
            AppController controller = new AppController();

            form.Add("AK47");
            form.Add("16");
            form.Add("123");
            form.Add("V12");
            form.Add("1");

            controller.NewEndpoin(out string erro, form);

            Assert.AreEqual("", erro, "New endpoint fail!");
        }


        [TestMethod]
        public void TestFindEndPoint()
        {
            List<string> form = new List<string>();
            AppController controller = new AppController();

            form.Add("AK47");
            form.Add("16");
            form.Add("123");
            form.Add("V12");
            form.Add("1");

            controller.NewEndpoin(out _, form);
            controller.FindEndpoint(form[0], out string erro);

            Assert.AreEqual("", erro, "Find EndPoint fail!");
        }


        [TestMethod]
        public void TestRemoveEndponit()
        {
            List<string> form = new List<string>();
            AppController controller = new AppController();

            form.Add("AK47");
            form.Add("16");
            form.Add("123");
            form.Add("V12");
            form.Add("1");

            controller.NewEndpoin(out _, form);
            controller.FindEndpoint(form[0], out _);
            controller.DeleteEndPoint();

            int count = controller.ListAll().Count;


            Assert.AreEqual(0, count, "Remove Endpoint Fail!!!");
        }


        [TestMethod]
        public void TestEditEndponit()
        {
            List<string> form = new List<string>();
            AppController controller = new AppController();

            form.Add("AK47");
            form.Add("16");
            form.Add("123");
            form.Add("V12");
            form.Add("1");

            controller.NewEndpoin(out _, form);
            controller.FindEndpoint(form[0], out _);
            controller.EditEndPoint("2");
            controller.FindEndpoint(form[0], out _ );

            int state = controller.SelectedEndpoint.SwitchState;

            Assert.AreEqual(2, state, "Edit Endpoint Fail!!!");

        }



    }
}