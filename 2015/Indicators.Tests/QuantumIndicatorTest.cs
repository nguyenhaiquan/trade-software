using commonClass;
// <copyright file="QuantumIndicatorTest.cs" company="cesti">Copyright © cesti 2011</copyright>

using System;
using Indicators;
using Microsoft.Pex.Framework;
using Microsoft.Pex.Framework.Validation;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Indicators.Tests
{
    [TestClass]
    [PexClass(typeof(QuantumIndicator))]
    [PexAllowedExceptionFromTypeUnderTest(typeof(ArgumentException), AcceptExceptionSubtypes = true)]
    [PexAllowedExceptionFromTypeUnderTest(typeof(InvalidOperationException))]
    public partial class QuantumIndicatorTest
    {

        [PexMethod]
        public QuantumIndicator Series(DataSeries ds, string name)
        {
            QuantumIndicator result = QuantumIndicator.Series(ds, name);
            return result;
            // TODO: add assertions to method QuantumIndicatorTest.Series(DataSeries, String)
        }

        /// <summary>Test stub for .ctor(DataSeries, String)</summary>
        [PexMethod]
        [PexAllowedException(typeof(NullReferenceException))]
        public QuantumIndicator ConstructorTest(DataSeries ds, string name)
        {
            QuantumIndicator target = new QuantumIndicator(ds, name);
            return target;
            // TODO: add assertions to method QuantumIndicatorTest.ConstructorTest(DataSeries, String)
        }
    }
}
