using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

namespace Kogane.Internal
{
    internal sealed class CheckRequiredOnEnterPlayModeSettingProvider : SettingsProvider
    {
        private const string PATH = "Kogane/Check Required On Enter Play Mode";

        private Editor m_editor;

        private CheckRequiredOnEnterPlayModeSettingProvider
        (
            string              path,
            SettingsScope       scopes,
            IEnumerable<string> keywords = null
        ) : base( path, scopes, keywords )
        {
        }

        public override void OnActivate( string searchContext, VisualElement rootElement )
        {
            var instance = CheckRequiredOnEnterPlayModeSetting.instance;

            instance.hideFlags = HideFlags.HideAndDontSave & ~HideFlags.NotEditable;

            Editor.CreateCachedEditor( instance, null, ref m_editor );
        }

        public override void OnGUI( string searchContext )
        {
            using var changeCheckScope = new EditorGUI.ChangeCheckScope();

            m_editor.OnInspectorGUI();

            EditorGUILayout.HelpBox( "Log Format で使用できるタグ", MessageType.Info );

            EditorGUILayout.TextArea
            (
                @"%GAME_OBJECT_NAME%
%GAME_OBJECT_HIERARCHY_PATH%
%COMPONENT_NAME%
%FIELD_NAME%"
            );

            if ( GUILayout.Button( "Reset to Default" ) )
            {
                Undo.RecordObject( CheckRequiredOnEnterPlayModeSetting.instance, "Reset to Default" );
                CheckRequiredOnEnterPlayModeSetting.instance.ResetToDefault();
            }

            if ( !changeCheckScope.changed ) return;

            CheckRequiredOnEnterPlayModeSetting.instance.Save();
        }

        [SettingsProvider]
        private static SettingsProvider CreateSettingProvider()
        {
            return new CheckRequiredOnEnterPlayModeSettingProvider
            (
                path: PATH,
                scopes: SettingsScope.Project
            );
        }
    }
}