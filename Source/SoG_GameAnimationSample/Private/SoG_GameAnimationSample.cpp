// Copyright SizeOf Games, All Rights Reserved.

#include "SoG_GameAnimationSample.h"

#include "GameplayTagsManager.h"
//#include "Misc/ConfigCacheIni.h"


#define LOCTEXT_NAMESPACE "FSoG_GameAnimationSampleModule"

void FSoG_GameAnimationSampleModule::StartupModule()
{
	// This code will execute after your module is loaded into memory; the exact timing is specified in the .uplugin file per-module
	UGameplayTagsManager::Get().AddTagIniSearchPath(FPaths::ProjectPluginsDir() / TEXT("SoG_GameAnimationSample/Config/Tags"));

	// FString DefaultEngineIni = FPaths::ProjectPluginsDir() / TEXT("GameAnimationSample/Config/DefaultEngine.ini");
	// if(!FConfigCacheIni::LoadGlobalIniFile(DefaultEngineIni,TEXT("DefaultEngine"), nullptr, true))
	// {
	// 	UE_LOG(LogTemp, Display,TEXT("Failed to load config ini file"));
	// }
}

void FSoG_GameAnimationSampleModule::ShutdownModule()
{
	// This function may be called during shutdown to clean up your module.  For modules that support dynamic reloading,
	// we call this function before unloading the module.
}

#undef LOCTEXT_NAMESPACE
	
IMPLEMENT_MODULE(FSoG_GameAnimationSampleModule, SoG_GameAnimationSample)