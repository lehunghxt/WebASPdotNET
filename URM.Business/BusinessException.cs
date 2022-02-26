namespace URM.Business
{
    using log4net;
    using System;
    using System.Diagnostics;
    using Library;

    /// <summary>
    /// The Provider exception.
    /// </summary>
    [DebuggerNonUserCode]
    [Serializable]
    public class BusinessException : ApplicationException
    {
        protected static readonly ILog log = LogManager.GetLogger(typeof(BusinessException));

        /// <summary>
        /// Initializes a new instance of the <see cref="BusinessLogicLayerException"/> class.
        /// </summary>
        [Obsolete("This constructor need for generic usages, use constructor with parameters instead.", true)]
        public BusinessException()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="BusinessLogicLayerException"/> class.
        /// </summary>
        /// <param name="message">
        /// The message.
        /// </param>
        public BusinessException(string message)
            : base(message)
        {
            throw new Exception(message);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="BusinessLogicLayerException"/> class.
        /// </summary>
        /// <param name="message">
        /// The message.
        /// </param>
        /// <param name="ex">
        /// The ex.
        /// </param>
        public BusinessException(string message, Exception ex)
            : base(message, ex)
        {
            log.Error(ex.TraceInformation());
            throw new Exception(message);
        }
    }
}