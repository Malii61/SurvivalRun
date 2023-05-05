using System.Collections.Generic;

public class MathematicalOperations
{
    public List<string> positiveOperationsList = new List<string>()
    {
        "x+x/3",
        "x + 25",
        "3*x+10",
        "5 * x",
        "10 * x",
        "20*x-50",
        "30*x-100",
        "2*x-10", 
        "4*x-30",
        "x/2+15",
        "2*x*sin90",
        "2*x*cos0",
        "30+3*x",
        "300+2*x", 
        "100+x",
    };
    public List<string> negativeOperationsList = new List<string>()
    {
        "(x+50)/8", 
        "(x+90)/10",
        "x - 30", 
        "300-2*x", //0 prob
        "100-x", //0 prob
        "2*x*sin0",//0
        "2*x*cos90", //0
        "x/3+10",
        "x-x/4",
        "x/5+25",
        "x/10",
        "x/2",
        "30-3*x",
        "x-x/3",
    };
}
