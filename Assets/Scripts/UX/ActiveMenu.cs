using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SIMS.SideQuest.ARKubb{
    public enum MenuType{
        Menu,
        Help,
        Sound
    }
    public static class ActiveMenu{
        public static MenuType currentMenu { get; set; }
    }
}
