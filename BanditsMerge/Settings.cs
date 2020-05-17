using System.Xml.Serialization;
using ModLib.Definitions;
using ModLib.Definitions.Attributes;

namespace BanditsMerge
{
    public class Settings : SettingsBase
    {
        public const string InstanceId = "BanditsMergeSettings";
        public override string ModName => "Bandits Merge";
        public override string ModuleFolderName => SubModule.ModuleFolderName;

        [XmlElement] public override string ID { get; set; } = InstanceId;

        public static Settings Instance => (Settings) SettingsDatabase.GetSettings<Settings>();

        [XmlElement]
        [SettingPropertyAttribute("Radius", 500, 5000)]
        public int Radius { get; set; } = 3000;

        [XmlElement]
        [SettingPropertyAttribute("BanditNumber", 0, 20)]
        public int BanditNumber { get; set; } = 10;

        [XmlElement]
        [SettingProperty("Merge Enabled")]
        public bool MergeEnabled { get; set; } = true;
    }
}