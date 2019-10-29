using Main.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace Main.Interface
{
    public interface ISystemConfigureBusiness
    {
        SystemConfigure GetSystemConfigure();
        bool SaveConfigure(SystemConfigure model);
    }
}
