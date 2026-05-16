# Elemental Wars - Setup Guide

## Installation Steps

### 1. Unity Project Setup
- Open Unity Hub
- Click "Add Project"
- Select the elemental-wars-main folder
- Choose Unity 2022.3 LTS or newer
- Wait for the project to load

### 2. Required Settings

#### Player Settings
1. Go to File > Build Settings
2. Add scenes:
   - Assets/Scenes/MainScene
   - Assets/Scenes/BattleScene
   - Assets/Scenes/MenuScene

#### Input Manager
1. Go to Edit > Project Settings > Input Manager
2. Configure inputs:
   - Movement: WASD or Analog Stick
   - Attack: Left Mouse / Gamepad Button A
   - Dodge: Space / Gamepad Button B
   - Power 1-8: Number Keys 1-8 / Gamepad Triggers

### 3. Audio Setup
- Create Audio folder in Assets
- Add background music and SFX
- Configure AudioManager prefab

### 4. Graphics Settings
1. Go to Edit > Project Settings > Quality
2. Recommended settings:
   - Resolution: 1920x1080
   - Target FPS: 60
   - Anti-aliasing: 4x MSAA

### 5. Monetization Setup
1. Configure MonetizationManager in GameManager prefab
2. Set currency exchange rates
3. Configure Game Pass tiers

## Testing the Game

1. Open MainScene from Assets/Scenes
2. Click the Play button in Unity
3. Test basic gameplay:
   - Movement with WASD
   - Combat with mouse clicks
   - Elemental powers with number keys

## Building for Different Platforms

### Windows/Mac/Linux
1. File > Build Settings
2. Choose PC platform
3. Click Build

### Mobile (iOS/Android)
1. Install Android SDK or Xcode
2. File > Build Settings
3. Choose Mobile platform
4. Configure player settings
5. Build and deploy

### Console (PlayStation/Xbox)
1. Register with platform developer programs
2. Install platform-specific SDKs
3. Configure console settings in Unity
4. Build using console build tools

## Troubleshooting

**Project won't load:**
- Check Unity version (2022.3+)
- Verify all folders exist
- Check console for errors

**Scripts have errors:**
- Reimport assets: Assets > Reimport All
- Check script references
- Verify namespaces

**Game runs slow:**
- Lower graphics quality
- Reduce draw calls
- Optimize scripts

## Next Steps
1. Read BOSS_DESIGN.md for boss mechanics
2. Review GAMEPASS_SYSTEM.md for monetization
3. Start developing game content
4. Create custom scenes and prefabs
