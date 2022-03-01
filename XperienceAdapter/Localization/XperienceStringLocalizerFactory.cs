using Microsoft.Extensions.Localization;
using System;
using System.Collections.Generic;
using System.Text;

namespace XperienceAdapter.Localization
{
    //Create a corresponding factory class
    public class XperienceStringLocalizerFactory : IStringLocalizerFactory
    {
        /// <summary>
        /// For both of the Create methods specified the interface, simply return a new instance of the XperienceStringLocalizer class.
        /// </summary>
        /// <param name="resourceSource"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public IStringLocalizer Create(Type resourceSource) =>
                new XperienceStringLocalizer();

        /// <summary>
        /// For both of the Create methods specified the interface, simply return a new instance of the XperienceStringLocalizer class.
        /// </summary>
        /// <param name="baseName"></param>
        /// <param name="location"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public IStringLocalizer Create(string baseName, string location) =>
                 new XperienceStringLocalizer();
    }
}
