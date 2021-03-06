// <copyright file="PexAssemblyInfo.cs" company="cesti">Copyright © cesti 2011</copyright>
using Microsoft.Pex.Framework.Coverage;
using Microsoft.Pex.Framework.Creatable;
using Microsoft.Pex.Framework.Instrumentation;
using Microsoft.Pex.Framework.Settings;
using Microsoft.Pex.Framework.Validation;

// Microsoft.Pex.Framework.Settings
[assembly: PexAssemblySettings(TestFramework = "VisualStudioUnitTest")]

// Microsoft.Pex.Framework.Instrumentation
[assembly: PexAssemblyUnderTest("Indicators")]
[assembly: PexInstrumentAssembly("System.Drawing")]
[assembly: PexInstrumentAssembly("common")]
[assembly: PexInstrumentAssembly("commonTypes")]
[assembly: PexInstrumentAssembly("application")]
[assembly: PexInstrumentAssembly("commonClass")]
[assembly: PexInstrumentAssembly("TA-Lib-Core")]

// Microsoft.Pex.Framework.Creatable
[assembly: PexCreatableFactoryForDelegates]

// Microsoft.Pex.Framework.Validation
[assembly: PexAllowedContractRequiresFailureAtTypeUnderTestSurface]
[assembly: PexAllowedXmlDocumentedException]

// Microsoft.Pex.Framework.Coverage
[assembly: PexCoverageFilterAssembly(PexCoverageDomain.UserOrTestCode, "System.Drawing")]
[assembly: PexCoverageFilterAssembly(PexCoverageDomain.UserOrTestCode, "common")]
[assembly: PexCoverageFilterAssembly(PexCoverageDomain.UserOrTestCode, "commonTypes")]
[assembly: PexCoverageFilterAssembly(PexCoverageDomain.UserOrTestCode, "application")]
[assembly: PexCoverageFilterAssembly(PexCoverageDomain.UserOrTestCode, "commonClass")]
[assembly: PexCoverageFilterAssembly(PexCoverageDomain.UserOrTestCode, "TA-Lib-Core")]

