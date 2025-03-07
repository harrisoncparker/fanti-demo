# Fanti Feature Development Plan

## ! This project plan was generated using AI and needs consideration before follwing it to the letter !

## Phase 1: Fanti Home Environment Enhancement
**Goal**: Improve the bookshelf home environment to make it more engaging and interactive

### 1. Fanti Movement System / Fanti Home AI
- [ ] Read and understand the current `Assets/Scripts/MonoBehaviours/FantiHomeAI.cs`
- [ ] Create mood-based behaviour/animation patterns (Maybe a modular system?)
- [ ] Let user drag and drop fanti around the home shelves (pause animations while doing it and don't clash with clicking on a fanti to open the overlay menu)

### 2. Poop System
- [ ] Implement poop generation timer
- [ ] Add poop cleanup interaction
- [ ] Create reward system for cleaning
- [ ] Add mood impact from uncleaned poop

### 3. Visual Indicators
- [ ] Add exclamation mark for unplayed cards
- [ ] Implement mood visualization
- [ ] Show study streak indicators
- [ ] Display available rewards

## Phase 2: Bookshelf Environment
**Goal**: Create a customizable and engaging home environment

### 1. Tile-Based System
- [ ] Implement 10x16 tile grid system
- [ ] Create tile placement validation
- [ ] Add item placement system
- [ ] Implement save/load for tile arrangements

### 2. Inventory System
- [ ] Create inventory data structure
- [ ] Implement item management
- [ ] Add categorization (furniture, decorations, etc.)
- [ ] Create UI for inventory management

## Phase 3: Fanti Customization
**Goal**: Enhance the pet customization experience

### 1. Wearable System
- [ ] Implement wearable slots (hats, accessories, outfits)
- [ ] Create wearable preview system
- [ ] Add save/load for Fanti customization
- [ ] Implement wearable effects on mood/stats

### 2. Shop System
- [ ] Create shop interface
- [ ] Implement currency system
- [ ] Add purchase validation
- [ ] Create inventory update system

## Phase 4: Mood and Streak System
**Goal**: Implement core engagement mechanics

### 1. Mood System
- [ ] Create mood calculation based on various factors
- [ ] Implement mood effects on Fanti behavior
- [ ] Add mood recovery mechanics
- [ ] Create mood visualization

### 2. Streak System
- [ ] Implement daily streak tracking
- [ ] Create milestone rewards
- [ ] Add streak visualization
- [ ] Implement streak recovery mechanics

## Technical Implementation Details

### Data Structures

```csharp
public class HomeLayout
{
    public Dictionary<Vector2Int, TileItem> TileItems { get; set; }
    public List<FantiPosition> FantiPositions { get; set; }
}

public class InventorySystem
{
    public Dictionary<string, InventoryItem> Items { get; set; }
    public int Currency { get; set; }
}

public class FantiCustomization
{
    public string HeadWearableId { get; set; }
    public string OutfitId { get; set; }
    public List<string> AccessoryIds { get; set; }
}
```

### Event System Integration

```csharp
// Add new events to existing GameEvent system
public static class GameEvents
{
    public static GameEvent OnPoopCleaned;
    public static GameEvent OnMoodChanged;
    public static GameEvent OnStreakUpdated;
    public static GameEvent OnInventoryChanged;
}
```

## Testing Strategy

1. **Unit Tests**
   - Movement system validation
   - Mood calculation
   - Inventory management
   - Streak tracking

2. **Integration Tests**
   - Fanti-environment interaction
   - Save/load system
   - Currency and purchase flow
   - Customization system

3. **User Testing**
   - Movement feel and responsiveness
   - UI clarity and ease of use
   - Reward satisfaction
   - Overall engagement

## Development Priorities

### High Priority
1. Fanti movement and basic interaction
2. Mood and streak core systems
3. Basic inventory and customization

### Medium Priority
1. Poop system
2. Advanced movement patterns
3. Shop implementation

### Low Priority
1. Advanced customization options
2. Additional animations
3. Special effects and polish

## Timeline Estimates

- **Phase 1**: 2-3 weeks
- **Phase 2**: 2 weeks
- **Phase 3**: 2 weeks
- **Phase 4**: 1-2 weeks
- **Testing & Polish**: 1-2 weeks

Total estimated time: 8-11 weeks

## Success Metrics

1. **Engagement Metrics**
   - Daily active users
   - Average session length
   - Return rate
   - Streak maintenance rate

2. **Learning Metrics**
   - Cards reviewed per session
   - Review completion rate
   - Long-term retention rate

3. **Technical Metrics**
   - Performance benchmarks
   - Error rates
   - Save/load reliability 