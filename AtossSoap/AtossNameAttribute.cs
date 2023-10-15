using System.Reflection;
using AtossSoap.ATCWebService;

namespace AtossSoap;

class AtossNameAttribute : Attribute {
    public string Name { get; set; }

    public AtossNameAttribute(string name) {
        Name = name;
    }
}

