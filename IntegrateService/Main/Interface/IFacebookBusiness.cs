using Main.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace Main
{
    public interface IFacebookBusiness
    {

        bool ReplyComment(ReplyComment model);
    }
}
