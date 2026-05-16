# Elemental Wars - Game Pass Monetization System

## Overview
The Game Pass system provides three tiers of progression and cosmetic benefits to support ongoing development.

## Subscription Tiers

### Free Tier
- **Cost**: Free
- **Benefits**:
  - Access to all 8 elemental powers
  - 4 boss battles
  - 1x progression multiplier
  - 2 free cosmetic skins
  - Limited cosmetic shop access
  - Community features

### Premium Tier
- **Cost**: $4.99/month or $39.99/year
- **Benefits**:
  - Everything in Free tier
  - Early access to new bosses (1 week early)
  - 2x progression multiplier
  - 5 exclusive premium skins
  - 10% discount on cosmetic shop
  - VIP badge in community
  - Priority matchmaking
  - Monthly cosmetic reward
  - Ad-free experience

### Premium Plus Tier
- **Cost**: $9.99/month or $79.99/year
- **Benefits**:
  - Everything in Premium tier
  - 3x progression multiplier
  - Unlimited cosmetic access
  - 25 exclusive elite skins
  - 20% discount on cosmetic shop
  - Elite badge and custom name color
  - Priority customer support
  - Weekly cosmetic reward
  - Battle Pass included
  - Special cosmetic effects
  - Early access to seasonal content

## Battle Pass System

### Free Battle Pass
- 50 levels
- 25 free rewards
- Basic cosmetics
- In-game currency

### Premium Battle Pass (Premium tier)
- 50 levels
- All 50 paid rewards
- 1000 premium currency
- Exclusive cosmetics
- Rare effects
- Seasonal theme items

### Elite Battle Pass (Premium Plus tier)
- 50 levels
- All 50 elite rewards
- 2000 premium currency
- Legendary cosmetics
- Unique effects
- Exclusive animations

## Cosmetic Shop

### Cosmetic Categories

#### Character Skins
- **Common**: 500 gems
- **Rare**: 1,500 gems
- **Epic**: 3,000 gems
- **Legendary**: 7,500 gems

#### Effects and Animations
- **Attack Effects**: 200-1,000 gems
- **Ultimate Effects**: 500-2,000 gems
- **Victory Animations**: 300-1,500 gems

#### Cosmetic Bundles
- **Seasonal Bundles**: 5,000-10,000 gems
- **Character Bundles**: 8,000-15,000 gems
- **Complete Collection**: 25,000 gems

## Currency System

### Free Currency (Gold)
- Earned from:
  - Boss battles: 100-500 gold
  - Daily quests: 50-200 gold
  - Weekly challenges: 500-2,000 gold
  - Seasonal events: 1,000-5,000 gold
- Used for: In-game upgrades, basic cosmetics

### Premium Currency (Gems)
- Purchased with real money:
  - 500 gems: $4.99
  - 1,100 gems: $9.99 (10% bonus)
  - 2,500 gems: $19.99 (11% bonus)
  - 5,500 gems: $39.99 (12% bonus)
- Used for: Premium cosmetics, battle pass
- Earned from: Premium tier monthly rewards

## Revenue Model

### Monthly Recurring Revenue (MRR)
- Premium subscriptions: $4.99-9.99/month
- Battle Pass (seasonal): $9.99/season
- One-time cosmetic purchases: $2-20

### Annual Recurring Revenue (ARR)
- Premium yearly: $39.99-79.99/year
- Battle Pass seasons: 4 per year
- Cosmetic bundles: $5-25

## Player Progression

### Level System
- Levels 1-100
- XP gained from battles and quests
- Multiplier based on tier:
  - Free: 1x XP
  - Premium: 2x XP
  - Premium Plus: 3x XP

### Milestone Rewards
- Level 10: 100 gems
- Level 25: 250 gems + exclusive skin
- Level 50: 500 gems + exclusive mount
- Level 75: 750 gems + title
- Level 100: 1,000 gems + legendary cosmetic

## Implementation Code

See `Scripts/Monetization/MonetizationManager.cs` for implementation.

### Key Methods
- `GetTierBenefits()` - Returns benefits for tier
- `CalculateProgression()` - Applies multiplier
- `PurchaseCosmeticItem()` - Handles cosmetic purchase
- `ClaimBattlePassReward()` - Claims battle pass reward

## Analytics

Track:
- Conversion rate (free to paid)
- Average revenue per user (ARPU)
- Customer lifetime value (CLV)
- Churn rate
- Battle Pass completion rate

## Future Additions
- Seasonal cosmetics
- Special events and tournaments
- Limited-time offers
- Cosmetic trading system
- Gifting system
