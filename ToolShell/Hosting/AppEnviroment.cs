using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToolShell.Wpf;

public static class AppEnvironment
{
#if DEBUG
    public static bool DevelopmentMode => true;
#else
    public static bool DevelopmentMode => false;
#endif
}
