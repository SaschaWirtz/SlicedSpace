using System.Collections;
using System.Collections.Generic;

public class GlobalInformation
{
    private static GlobalInformation _instance = null;

    public bool introPlayed { get; set; } = false;

    private GlobalInformation() {

    }

    public static GlobalInformation getInstance() {
        if (_instance == null) {
            _instance = new GlobalInformation();
        }
        
        return _instance;
    }
}
