using UnityEditor;
using UnityEngine;

namespace Kogane.Internal
{
    /// <summary>
    /// 設定を管理するクラス
    /// </summary>
    [FilePath( "UserSettings/Kogane/CheckRequiredOnEnterPlayMode.asset", FilePathAttribute.Location.ProjectFolder )]
    internal sealed class CheckRequiredOnEnterPlayModeSetting : ScriptableSingleton<CheckRequiredOnEnterPlayModeSetting>
    {
        private const bool   DEFAULT_IS_ENABLE  = false;
        private const string DEFAULT_LOG_FORMAT = "「%GAME_OBJECT_HIERARCHY_PATH%」の「%FIELD_NAME%」フィールドに参照が設定されていません";

        [SerializeField]                    private bool   m_isEnable  = DEFAULT_IS_ENABLE;
        [SerializeField][TextArea( 5, 10 )] private string m_logFormat = DEFAULT_LOG_FORMAT;

        public bool   IsEnable  => m_isEnable;
        public string LogFormat => m_logFormat;

        public void Save()
        {
            Save( true );
        }

        public void ResetToDefault()
        {
            m_isEnable  = DEFAULT_IS_ENABLE;
            m_logFormat = DEFAULT_LOG_FORMAT;
        }
    }
}