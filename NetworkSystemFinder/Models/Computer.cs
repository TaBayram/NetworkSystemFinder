using NetworkSystemFinder.Helpers;
using NetworkSystemFinder.Models.Parts;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetworkSystemFinder.Models
{
    public class Computer:Machine
    {
        CPU oCPU = new Parts.CPU();
        OS oOS = new Parts.OS();
        List<GPU> gPUs = new List<GPU>() { new Parts.GPU() };
        List<RAM> rAMs = new List<RAM>() { new Parts.RAM() };
        List<Storage> storages = new List<Storage>();
        BIOS oBIOS = new BIOS();
        Motherboard oMotherboard = new Parts.Motherboard();
        Network oNetwork = new Network();
        Account oAccount = new Account();


        public Computer(string IP)
        {
            this.iP = IP;
            this.name = "?";

        }

        [Browsable(true)]  public new string IP { get => iP; set => iP = value; }
        [Browsable(true)] public new string Name { get => name; set => name = value; }
        [Browsable(true)] public new Machine.StatusType Status { get => status; set => status = value; }
        public string OS { get => OOS.Model;}
        public string CPU { get => OCPU.Model; }
        public int RAM { get => TotalRamCapacity(); }
        public string RAMType { get => RAMs[0].Type; }
        public string GPU { get => TryGetExternalGPU(); }
        public int HDD { get => GetStorageCapacity(Storage.StorageType.HDD); }
        public int SSD { get => GetStorageCapacity(Storage.StorageType.SSD); }
        [Browsable(true)] public new string SerialNumber { get => OBIOS.SerialNumber; }
        [Browsable(true)] public new string MAC { get => ONetwork.MACAddress; set => ONetwork.MACAddress = value; }
        public string Motherboard { get => oMotherboard.Model; }


        [Browsable(false)] public CPU OCPU { get => oCPU; set => oCPU = value; }
        [Browsable(false)] public OS OOS { get => oOS; set => oOS = value; }
        [Browsable(false)] public List<RAM> RAMs { get => rAMs; set => rAMs = value; }
        [Browsable(false)] public List<Storage> Storages { get => storages; set => storages = value; }
        [Browsable(false)] public BIOS OBIOS { get => oBIOS; set => oBIOS = value; }
        [Browsable(false)] public Motherboard OMotherboard { get => oMotherboard; set => oMotherboard = value; }
        [Browsable(false)] public Network ONetwork { get => oNetwork; set => oNetwork = value; }
        [Browsable(false)] public Account OAccount { get => oAccount; set => oAccount = value; }
        [Browsable(false)] public List<GPU> GPUs { get => gPUs; set => gPUs = value; }

     
        public string[] SplitName()
        {
            if (this.CPU == "Unknown") return new string[] { "Unknown" };
            string[] name = new string[2];
            string wholeName = this.OCPU.Model;

            int numberCounter = 0;
            int index = 0;
            bool hasFound = false;
            for(int i = 0; i < wholeName.Length; i++)
            {
                char c = wholeName[i];
                bool isInt = int.TryParse(""+c,out int _);
                if (isInt) 
                {
                    if(numberCounter == 0)
                    {
                        index = i;
                    }
                    numberCounter++;
                }
                else numberCounter = 0;
                if(numberCounter >= 3)
                {
                    hasFound = true;
                    index--;
                    break;
                }
            }

            if (hasFound)
            {
                while (wholeName[index] != ' ' && wholeName[index] != '-') index--;
                name[0] = wholeName.Substring(0, index).Replace("CPU", "");
                name[1] = wholeName.Substring(index+1);

                numberCounter = 0;
                index = 0;
                for(int i = name[0].Length-1; i >= 0; i--)
                {
                    if (name[0][i] == ' ')
                    {
                        numberCounter++;
                        if(numberCounter > 2)
                        {
                            name[0] = name[0].Substring(0, i).Trim();
                            break;
                        }
                    }
                }
            }

            return name;
        }

        private int TotalRamCapacity()
        {
            int i = 0;
            foreach(RAM ram in RAMs)
            {
                i += ram.Capacity;
            }
            return i;
        }

        private string TryGetExternalGPU()
        {
            if(GPUs.Count > 1)
            {
                return GPUs[1].Model;
            }
            return GPUs[0].Model;
        }

        private int GetStorageCapacity(Storage.StorageType storageType)
        {
            int i = 0;
            foreach(Storage storage in storages)
            {
                if(storage.Type == storageType)
                {
                    i += storage.Capacity;
                }
            }
            return i;
        }
    }
}
