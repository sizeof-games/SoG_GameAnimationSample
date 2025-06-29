// Copyright Epic Games, Inc. All Rights Reserved.

using System.IO;
using UnrealBuildTool;
using System.Collections.Generic;
using System.Linq;
using System;

public class SoG_GameAnimationSample : ModuleRules
{
	public SoG_GameAnimationSample(ReadOnlyTargetRules Target) : base(Target)
	{
		PCHUsage = ModuleRules.PCHUsageMode.UseExplicitOrSharedPCHs;

		PublicIncludePaths.AddRange(
			new string[]
			{
				// ... add public include paths required here ...
			}
		);


		PrivateIncludePaths.AddRange(
			new string[]
			{
				// ... add other private include paths required here ...
			}
		);


		PublicDependencyModuleNames.AddRange(
			new string[]
			{
				"Core",
				"GameplayTags",
				"DeveloperSettings"
				// ... add other public dependencies that you statically link with here ...
			}
		);


		PrivateDependencyModuleNames.AddRange(
			new string[]
			{
				"CoreUObject",
				"Engine",
				"Slate",
				"SlateCore",
				// ... add private dependencies that you statically link with here ...	
			}
		);


		DynamicallyLoadedModuleNames.AddRange(
			new string[]
			{
				// ... add any modules that your module loads dynamically here ...
			}
		);

		FillDefaultEngineIniWithDataDrivenCVars();
		FillDefaultInisWithCollisionProfile();
	}

	private void FillDefaultEngineIniWithDataDrivenCVars()
	{
		if (Target.ProjectFile != null)
		{
			string DefaultEngineIniPath =
				Path.GetFullPath(Path.Combine(Target.ProjectFile.Directory.ToString(), "Config", "DefaultEngine.ini"));

			string PluginDataDrivenConsoleVariableSettingsIniPath = Path.Combine(
				Target.ProjectFile.Directory.ToString(),
				"Plugins/SoG_GameAnimationSample/Config", "DataDrivenConsoleVariableSettings.ini");

			string PluginDataDrivenConsoleVariableSettingsIniContent =
				File.ReadAllText(PluginDataDrivenConsoleVariableSettingsIniPath);

			string DefaultEngineIniContent = File.ReadAllText(DefaultEngineIniPath);
			string[] PluginDataDrivenConsoleVariableSettingsContentArray =
				PluginDataDrivenConsoleVariableSettingsIniContent.Split('\n');

			if (!DefaultEngineIniContent.Contains(PluginDataDrivenConsoleVariableSettingsContentArray[0]))
			{
				Console.WriteLine("\nSoG_GameAnimationSample: DefaultEngineIniContent doesn't contains " +
				                  PluginDataDrivenConsoleVariableSettingsContentArray[0]);
				Console.WriteLine("SoG_GameAnimationSample: Adding entire Plugin's DefaultEngine Ini Content");

				File.AppendAllText(DefaultEngineIniPath, "\n\n");
				File.AppendAllText(DefaultEngineIniPath, PluginDataDrivenConsoleVariableSettingsIniContent);
			}
			else
			{
				Console.WriteLine("\nDefaultEngineIniContent contains " +
				                  PluginDataDrivenConsoleVariableSettingsContentArray[0]);

				string StringToReplaceInDefaultEngineIniContent =
					PluginDataDrivenConsoleVariableSettingsContentArray[0];

				for (int i = 1; i < PluginDataDrivenConsoleVariableSettingsContentArray.Length; i++)
				{
					string PluginDefaultEngineIniContentArrayElement =
						PluginDataDrivenConsoleVariableSettingsContentArray[i];
					Console.WriteLine("SoG_GameAnimationSample: checking " + PluginDefaultEngineIniContentArrayElement);

					if (!DefaultEngineIniContent.Contains(PluginDefaultEngineIniContentArrayElement))
					{
						Console.WriteLine("SoG_GameAnimationSample: " +
						                  "element " + PluginDefaultEngineIniContentArrayElement +
						                  " is absent. Adding...");

						StringToReplaceInDefaultEngineIniContent += PluginDefaultEngineIniContentArrayElement;
						StringToReplaceInDefaultEngineIniContent += "\n";
					}
					else
					{
						Console.WriteLine("SoG_GameAnimationSample: element " + PluginDefaultEngineIniContentArrayElement +
						                  " is present. Skipping...");
					}
				}

				DefaultEngineIniContent = DefaultEngineIniContent.Replace(
					PluginDataDrivenConsoleVariableSettingsContentArray[0],
					StringToReplaceInDefaultEngineIniContent);
				File.WriteAllText(DefaultEngineIniPath, DefaultEngineIniContent);
			}
		}
	}

	private void FillDefaultInisWithCollisionProfile()
	{
		if (Target.ProjectFile != null)
		{
			string DefaultEngineIniPath =
				Path.GetFullPath(Path.Combine(Target.ProjectFile.Directory.ToString(), "Config", "DefaultEngine.ini"));

			string PluginCollisionProfileSettingsIniPath = Path.Combine(Target.ProjectFile.Directory.ToString(),
				"Plugins/SoG_GameAnimationSample/Config", "CollisionProfileSettings.ini");

			string PluginCollisionProfileSettingsIniContent = File.ReadAllText(PluginCollisionProfileSettingsIniPath);

			string DefaultEngineIniContent = File.ReadAllText(DefaultEngineIniPath);
			string[] PluginDefaultEngineIniContentArray = PluginCollisionProfileSettingsIniContent.Split('\n');

			Dictionary<string, string> ParamsFromPluginIni = new Dictionary<string, string>();

			foreach (var PluginDefaultEngineIniContentArrayElem in PluginDefaultEngineIniContentArray)
			{
				string[] Param = PluginDefaultEngineIniContentArrayElem.Split('=');
				string Key = Param[0].Replace("\r", "").Replace("\n", "");
				string Val = Param[1].Replace("\r", "").Replace("\n", "");
				ParamsFromPluginIni.Add(Key, Val);
			}
			
			if (ParamsFromPluginIni["bForceAddTraversableCollisionChannelToDefaultEngineIni"]
			    .Contains("False", StringComparison.OrdinalIgnoreCase))
			{
				return;
			}

			Console.WriteLine(ParamsFromPluginIni["Name"].Replace("\"", ""));
			if (DefaultEngineIniContent.Contains(ParamsFromPluginIni["Name"]))
			{
				Console.WriteLine("\nDefaultEngineIniContent already contains contains " + ParamsFromPluginIni["Name"] +
				                  " DefaultChannelResponse");
				return;
			}
			

			string ParamSectionValue = ParamsFromPluginIni["Section"];


			string CollisionChannel = "";
			int CollisionChannelId = 0;
			
			string CollisionName = ParamsFromPluginIni["Name"];

			if (!DefaultEngineIniContent.Contains(ParamSectionValue))
			{
				CollisionChannel = "ECC_GameTraceChannel1";

				Console.WriteLine("\nSoG_GameAnimationSample: DefaultEngineIniContent doesn't contains " +
				                  ParamSectionValue);

				string DefaultChannelResponsesString =
					FormDefaultChannelResponsesString(CollisionChannel, ParamsFromPluginIni["DefaultResponse"],
						ParamsFromPluginIni["bTraceType"], ParamsFromPluginIni["bStaticObject"],
						CollisionName);

				string StringToAppend = "\n\n[/Script/Engine.CollisionProfile]\n" + DefaultChannelResponsesString;

				File.AppendAllText(DefaultEngineIniPath, StringToAppend);
			}
			else
			{
				Console.WriteLine("\nDefaultEngineIniContent contains " + ParamSectionValue);
				
				// find first free CollisionChannel
				int i = 1;
				bool bFound = false;
				for (; i <= 18; i++)
				{
					string StringToSearch = "ECC_GameTraceChannel" + i.ToString();
					if (!DefaultEngineIniContent.Contains(StringToSearch))
					{
						CollisionChannel = StringToSearch;
						CollisionChannelId = i;
						bFound = true;
						break;
					}
				}

				if (!bFound)
				{
					Console.WriteLine(
						"\nDefaultEngineIniContent contains all 18 ECC_GameTraceChannel. No spare space to add our");
					return;
				}
				
				DefaultEngineIniContent = DefaultEngineIniContent.Replace(
					ParamSectionValue,
					ParamSectionValue + "\n" + FormDefaultChannelResponsesString(CollisionChannel, ParamsFromPluginIni["DefaultResponse"],
						ParamsFromPluginIni["bTraceType"], ParamsFromPluginIni["bStaticObject"],
						CollisionName));
				File.WriteAllText(DefaultEngineIniPath, DefaultEngineIniContent);

				FillDefaultGameIniWithTraversalCollisionChannel(CollisionChannelId);
			}
		}
	}


	private string FormDefaultChannelResponsesString(string Channel, string DefaultResponse, string bTraceType,
		string bStaticObject, string Name)
	{
		string Result = ("+DefaultChannelResponses=(Channel=" + Channel + ",DefaultResponse=" + DefaultResponse +
		                 ",bTraceType=" + bTraceType + ",bStaticObject=" + bStaticObject + ",Name=" + Name + ")")
			.Replace("\r", "")
			.Replace("\n", "");
		
		Console.WriteLine(Result);
		
		return Result;
	}

	private void FillDefaultGameIniWithTraversalCollisionChannel(int Channel)
	{
		if (Target.ProjectFile != null)
		{
			string DefaultGameIniPath =
				Path.GetFullPath(Path.Combine(Target.ProjectFile.Directory.ToString(), "Config", "DefaultGame.ini"));
			
			Console.WriteLine("DefaultGameIniPath: " + DefaultGameIniPath);
			
			string DefaultGameIniContent = File.ReadAllText(DefaultGameIniPath);

			string Section = "[/Script/SoG_GameAnimationSample.SoG_GameAnimationSampleSettings]";
			string Key = "TraversalCollisionChannel";
			string Value = "ECC_GameTraceChannel";
			string KeyValue = Key + "=" + Value;
			
			if (DefaultGameIniContent.Contains(Section) &&
			    DefaultGameIniContent.Contains(KeyValue))
			{
				int Index = DefaultGameIniContent.IndexOf(KeyValue, StringComparison.CurrentCulture);
				int StringLength = KeyValue.Length;

				int CountToRemove = (DefaultGameIniContent[Index + StringLength + 1] == '\r') ? 1 : 2;
				DefaultGameIniContent = DefaultGameIniContent.Remove(Index + StringLength, CountToRemove);

				DefaultGameIniContent = DefaultGameIniContent.Insert(Index + StringLength, Channel.ToString());
				File.WriteAllText(DefaultGameIniPath, DefaultGameIniContent);
			}
			else if(!DefaultGameIniContent.Contains(Section))
			{
				string StringToAppend = "\r\r" + Section + "\r" + KeyValue + Channel.ToString();

				File.AppendAllText(DefaultGameIniPath, StringToAppend);
			}
			else if (DefaultGameIniContent.Contains(Section) && !DefaultGameIniContent.Contains(KeyValue))
			{
				int Index = DefaultGameIniContent.IndexOf(Section, StringComparison.CurrentCulture);
				int StringLength = Section.Length;

				DefaultGameIniContent =
					DefaultGameIniContent.Insert(Index + StringLength, "\r" + KeyValue + Channel.ToString());
				
				File.WriteAllText(DefaultGameIniPath, DefaultGameIniContent);
			}

		}
	}
}