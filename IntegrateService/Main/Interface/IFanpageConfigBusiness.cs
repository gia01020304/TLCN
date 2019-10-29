using Main.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace Main
{
    public interface IFanpageConfigBusiness
    {
        /// <summary>
        /// Get fanpage config
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        FanpageConfig GetFanpageConfig(FanpageConfigFilter filter);
        /// <summary>
        /// Get list fanpageconfig
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        List<FanpageConfig> GetListFanPageConfigure(FanpageConfigFilter filter);
    }
}
