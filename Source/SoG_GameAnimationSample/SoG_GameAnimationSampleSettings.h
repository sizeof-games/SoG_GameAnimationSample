// Copyright SizeOf Games, All Rights Reserved.

#pragma once

#include "CoreMinimal.h"
#include "Engine/DeveloperSettings.h"
#include "SoG_GameAnimationSampleSettings.generated.h"

class UObject;

/**
 * 
 */
UCLASS(config = Game, defaultconfig)
class SOG_GAMEANIMATIONSAMPLE_API USoG_GameAnimationSampleSettings : public UDeveloperSettings
{
	GENERATED_BODY()

public:
	USoG_GameAnimationSampleSettings(const FObjectInitializer& Initializer);
		
public:
	TEnumAsByte<ECollisionChannel> GetTraversalCollisionChannel() const { return TraversalCollisionChannel; };
		
protected:
	/**
	 * What trace channel should be used to find available targets for Traversal.
	 * @see UAimAssistTargetManagerComponent::GetVisibleTargets
	 */
	UPROPERTY(config, EditAnywhere, Category = "GameAnimationSample|Traversal")
	TEnumAsByte<ECollisionChannel> TraversalCollisionChannel = ECollisionChannel::ECC_WorldStatic;
};
