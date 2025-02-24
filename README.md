# Gluckbusters Game

#### Game name pending

Team members in surname alphabetical order:

- Osama Abuhilal
- Baqer Almaarfawi
- AJ Berman
- Marcial Machado
- Kevin Ravakhah

# State of the Project

#### Last Updated: 2/17/2025

The game largely functions as a proof-of-concept for various crucial functionalities
that will be present throughout the game's development.
The main player character, a red hooded sprite sourced from
[Penzilla Design](https://penzilla.itch.io/hooded-protagonist), is controllable in the
cardinal directions with the WASD or Arrow keys on a personal computer keyboard. 
There are three enemies, selectable through the keys `o` and `p`. All move in a random
way. The goblin and dragon enemies have a probability to attack; the goblin will throw
a boomerang, and the dragon will shoot fireballs. A block sprite near the center of
the game screen can be cycled through different available blocks with `t` and `y`.
The currently active user item is visible near the top of the screen and can be cycled
through different items with `u` and `i`.

### Expected Improvements

- Large-scale architectural refactoring.
    - Managers for various objects.
    - Dedicated renderer for drawing to the screen.
- Expanded player functionality

### Known limitations:

- Equipped items currently do not meaningfully change player behavior.

### Known bugs:

- Going diagonally, that is, holding keys such as SD, WD, etc simultaneously, will cause
the player sprite to freeze in one animation frame belonging to either of the two keys
pressed.
- Walking animations do not properly get rendered when using the arrow keys.
- Enemies are capable of leaving the visible screen.
- The player is capable of leaving the visible screen.

## Current Code Analytics

Code Metrics are run weekly on Sunday across the entire Project, and are tracked in the below sheet:

https://docs.google.com/spreadsheets/d/1jBsXMssNcBD1WS8oKJFyx0KgWlA-tytG65Kg3K-wn44/edit?usp=sharing

# Guidelines & Best Practices

## Naming Conventions

- **Pull Requests, Issues, Commits:** Brief single-sentence descriptor in an active imperative voice.
e.g. "Create Static Sprite for MC"
- **Branches:** Lowercase, hyphens for spaces.
- **Code:** Follow C# naming conventions as specified in Carmen.

## Code Reviews

- At least 2 reviewers for *every* Pull Requests (PR). 
- Reviewers should checkout into candidate branches, build, and run to ensure no errors.
- Feedback should be documented (whether errors are found or not) as comments; explicitly
defined code comments are preferred.

## Task & Priorities

Colloquially referred as the "Jira Board" within the team, a template under the "3902" has been created and should be used for all Tasks. 
Each Task should have an assigned priority at the beginning of every Sprint:

- P1: **Critical.** Critical issues should be addressed as soon as possible.

- P2: **Important.** Important Tasks are those that major functions of the game depend on. 

- P3: **Relevant.** Relevant Tasks are those that are not crucial but must be completed.

- P4: **Enhancements.** Enhancements are tasks not required for a Sprint, but if time allows can be implemented.

As issues are being worked on, estimates for the number of hours expected left before the
issue is resolved should be tracked.

## Communication

Discussion for each Task/Issue should mainly be kept on Github. 

For historical reasons, Tasks should be marked as Done when completed, never deleted.
