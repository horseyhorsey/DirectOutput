﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace DirectOutput.General
{
    /// <summary>
    /// This object provides information on a thread.
    /// </summary>
    public class ThreadInfo : IDisposable
    {
        #region IDisposable Member

        /// <summary>
        /// Cleans up the resources used by instances of this class.
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }


        /// <summary>
        /// Releases unmanaged and - optionally - managed resources.
        /// </summary>
        /// <param name="disposing"><c>true</c> to release both managed and unmanaged resources; <c>false</c> to release only unmanaged resources.</param>
        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                // free managed resources

            }
            // free native resources if there are any.
            Thread = null;
        }

        #endregion




        private Queue<Exception> _Exceptions = new Queue<Exception>();
        /// <summary>
        /// Gets a list of the last 30 exceptions which have been recorded using the RecordException method.
        /// </summary>
        /// <value>
        /// The list of exceptions.
        /// </value>
        public IList<Exception> Exceptions
        {
            get { return _Exceptions.ToList(); }
        }

        /// <summary>
        /// Adds a captured exception to the Expeptions list.
        /// </summary>
        /// <param name="Exception">The captured exception.</param>
        public void RecordException(Exception Exception)
        {
            _Exceptions.Enqueue(Exception);
            if (_Exceptions.Count > 30)
            {
                _Exceptions.Dequeue();
            }
        }


        /// <summary>
        /// Gets or sets the name of the object hosting the thread.
        /// </summary>
        /// <value>
        /// The name of the host object.
        /// </value>
        public string HostName { get; set; }

        /// <summary>
        /// Gets or sets the thread for which this object return information.
        /// </summary>
        /// <value>
        /// The thread.
        /// </value>
        public Thread Thread { get; set; }

        /// <summary>
        /// Gets the timestamp of the last heartbeat.
        /// </summary>
        /// <value>
        /// The last heart beat timestamp.
        /// </value>
        public DateTime LastHeartBeat { get; private set; }

        /// <summary>
        /// HeartBeat has to be called regularely to update the LastHeartBeat property.
        /// </summary>
        public void HeartBeat()
        {
            LastHeartBeat = DateTime.Now;
        }

        private int _HeartBeatTimeOutMs=1000;

        /// <summary>
        /// Gets or sets the heartbeat timeout in milliseconds.<br/>
        /// </summary>
        /// <value>
        /// The heartbeat timeout in milliseconds.
        /// </value>
        public int HeartBeatTimeOutMs
        {
            get { return _HeartBeatTimeOutMs; }
            set { _HeartBeatTimeOutMs = value; }
        }
        

       



        /// <summary>
        /// Gets the name of the thread.
        /// </summary>
        /// <value>
        /// The name of the thread.
        /// </value>
        public string ThreadName
        {
            get { return Thread.Name; }
        }

        /// <summary>
        /// Gets a value indicating whether the thread represented by this instance is alive.<be/>
        /// </summary>
        /// <value>
        ///   <c>true</c> if the thread is alive; otherwise, <c>false</c>.
        /// </value>
        public bool IsAlive
        {
            get
            {
                
                return Thread.IsAlive;
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ThreadInfo"/> class.
        /// </summary>
        /// <param name="Thread">The thread for which the ThreadInfo object is created.</param>
        public ThreadInfo(Thread Thread)
        {
            this.Thread = Thread;
            
 
            HeartBeat();

        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ThreadInfo"/> class for the thread creating the instance.
        /// </summary>
        public ThreadInfo()
            : this(Thread.CurrentThread)
        {

        }


        /// <summary>
        /// Finalizes an instance of the <see cref="ThreadInfo"/> class.
        /// </summary>
        ~ThreadInfo()
        {
            Dispose(false);
        }


    }
}
