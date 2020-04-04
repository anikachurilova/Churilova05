using Microsoft.VisualBasic.Devices;
using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Management;
using System.Runtime.CompilerServices;
using System.Windows;


namespace Churilova05.Models
{
    public class TaskProcess : INotifyPropertyChanged
    {
        #region Fields


        private readonly PerformanceCounter _counter;
        private readonly Process _process;
        
        private float _cpu;
        private long _workingSet64;

        private readonly DateTime _startDateTime;

        private double _percentOfMemory = -1;
        private long _turnOn = -1;

        #endregion

        #region Properties

        public int Id { get; set; }
        public string Path { get; set; }
        public string Name { get; set; }

        public DateTime StartDateTime => _startDateTime;
        public string Cpu => _cpu.ToString("0.00") + "%";
        public bool IsActive => _process.Responding;
        public int CountThreads => _process?.Threads.Count ?? 0;
        public string Ram => (_workingSet64 * _percentOfMemory).ToString("0.00") + "%";
        public long MemoryWorkingSet64 => _workingSet64;

        

        public ProcessModuleCollection GetModules
        {
            get
            {
                try
                {
                    ProcessModuleCollection arr = _process?.Modules;
                    return arr;
                }

                catch (Exception) { return null; }


            }

        }

        public ProcessThreadCollection GetThreads
        {
            get
            {
                try
                {
                    ProcessThreadCollection arr = _process?.Threads;

                    return arr;
                }
                catch (Exception) { return null; }

            }
        }

        #endregion


        #region Commands

        public void Kill()
        {
            try { _process.Kill(); }
            catch (Exception e) { MessageBox.Show(e.Message); }
        }

        public void Update()
        {
            _workingSet64 = _process.WorkingSet64;
            try
            {
                if (_turnOn == -1) {
                    _turnOn = DateTimeOffset.UtcNow.ToUnixTimeMilliseconds();
                    _cpu = 0;
                    _counter.NextValue();
                }
                else if (DateTimeOffset.UtcNow.ToUnixTimeMilliseconds() - _turnOn > 1000) {
                    _turnOn = DateTimeOffset.UtcNow.ToUnixTimeMilliseconds();
                    _cpu = _counter.NextValue() / Environment.ProcessorCount;
                }

            }
            catch (Exception) { }

            OnPropertyChanged($"MemoryWorkingSet64");
            OnPropertyChanged($"Ram");
            OnPropertyChanged($"Cpu");

        }

        #endregion

        #region Constructors

        public TaskProcess(Process process)
        {

            _process = process;
            Name = process.ProcessName;
            Id = process.Id;
    

            try { Path = process.MainModule.FileName; }
            catch (Exception) { Path = "Acces denied"; }


            _counter =
                new PerformanceCounter("Process", "% Processor Time", process.ProcessName, process.MachineName);
            _percentOfMemory = 100.0 / (new ComputerInfo()).TotalPhysicalMemory;

            try {_startDateTime = _process.StartTime; }
            catch (Exception) { }
           
        }

        #endregion


        #region INotifyPropertyChanged

        public event PropertyChangedEventHandler PropertyChanged;


        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        #endregion
    }

    
}