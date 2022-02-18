using System;
using System.Collections.Generic;
using System.Text;

namespace XperienceAdapter.Localization
{
    /// <summary>
    /// To comply with the requirements enforced by the localization infrastructure of ASP.NET Core, we must also create an empty class that serves as a namespace denominator.
    /// If we were to implement a string localizer that uses .resx files, the class would help identify the files.
    /// In this case, we only need to create it as a formal requirement.
    /// </summary>
    public class SharedResource
    {
    }
}
