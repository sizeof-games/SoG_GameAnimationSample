// Copyright SizeOf Games, All Rights Reserved.

#pragma once

#include "Modules/ModuleManager.h"

class FSoG_GameAnimationSampleModule : public IModuleInterface
{
public:

	/** IModuleInterface implementation */
	virtual void StartupModule() override;
	virtual void ShutdownModule() override;
};
