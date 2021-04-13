using System;
using System.IO.Ports;
using System.Text.RegularExpressions;

namespace ArduinoControl.library
{
    public class SerialPortConnector
    {
        private readonly int _baudRate = 9600;
        private readonly string _portName = "COM3";

        public bool Send(string command, string roomNumber)
        {
            using(var serialPort = new SerialPort(_portName, _baudRate))
            {
                serialPort.Open();
                serialPort.Write(command);

                string data = Commons.FileUtils.read("minhhanh.dat", "");
                string[] datas = Commons.FileUtils.read("minhhanh.dat", "").Split(',');

                datas[Convert.ToInt32(roomNumber) - 1] = command;

                return Commons.FileUtils.write("minhhanh.dat", String.Join(',', datas));
            }
        }
    }
}
