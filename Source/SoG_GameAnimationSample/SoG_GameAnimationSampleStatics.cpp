// Copyright SizeOf Games, All Rights Reserved.

#include "SoG_GameAnimationSampleStatics.h"

#include "SoG_GameAnimationSampleSettings.h"

TEnumAsByte<ECollisionChannel> USoG_GameAnimationSampleStatics::GetTraceChannel_Traversable()
{
	USoG_GameAnimationSampleSettings const* GameAnimationSampleSettingsCDO = GetDefault<USoG_GameAnimationSampleSettings>();
	return GameAnimationSampleSettingsCDO->GetTraversalCollisionChannel();

	//return static_cast<TEnumAsByte<ECollisionChannel>>(0);
}

ETraceTypeQuery USoG_GameAnimationSampleStatics::GetTraceTypeQuery_Traversable()
{
	return UEngineTypes::ConvertToTraceType(GetTraceChannel_Traversable().GetValue());
}
