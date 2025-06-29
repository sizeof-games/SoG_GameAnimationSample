// Copyright SizeOf Games, All Rights Reserved.

#pragma once

#include "CoreMinimal.h"
#include "Kismet/BlueprintFunctionLibrary.h"
#include "SoG_GameAnimationSampleStatics.generated.h"

/**
 * 
 */
UCLASS()
class SOG_GAMEANIMATIONSAMPLE_API USoG_GameAnimationSampleStatics : public UBlueprintFunctionLibrary
{
	GENERATED_BODY()

	UFUNCTION(BlueprintPure, Category="GameAnimationSample")
	static TEnumAsByte<ECollisionChannel> GetTraceChannel_Traversable();

	UFUNCTION(BlueprintPure, Category="GameAnimationSample")
	static ETraceTypeQuery GetTraceTypeQuery_Traversable();
};
