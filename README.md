# Simple Clock

## Overview

Simple Clock is a project developed using the Unity game engine.
It synchronizes the current time with the Yandex time service upon startup.
If the synchronization request fails, it sets the local current time and starts the clock.
Additionally, the clock resynchronizes every hour. Manual adjustment of the current time is also possible.

## Features

- **Time Synchronization**: Automatically syncs with Yandex's time service on startup.
- **Local Time Fallback**: If synchronization fails, the clock initializes with the local time.
- **Hourly Resynchronization**: The clock re-synchronizes with the time service every hour.
- **Manual Time Adjustment**: Users can manually set the current time.
