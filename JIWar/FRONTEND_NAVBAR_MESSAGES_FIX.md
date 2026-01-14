# 🚀 FRONTEND FIX: Global Messages & Real-time Notifications

This document implements the **Global Messages State** required to sync the Navbar icon, unread count, and real-time notifications.

## 1. Create `GlobalMessagesService`
Create a new service: `src/app/services/global-messages.service.ts`

This service acts as the **single source of truth** for message notifications.

```typescript
import { Injectable } from '@angular/core';
import { BehaviorSubject, Observable } from 'rxjs';
import { HttpClient } from '@angular/common/http';
import * as signalR from '@microsoft/signalr';
import { environment } from 'src/environments/environment';
import { AuthService } from './auth.service'; // Assuming you have one

export interface ChatNotification {
  title: string;
  message: string;
  sentDate: Date;
  relatedId: string; // RequestID or PropertyID
  type: string;
  unreadCount?: number;
}

@Injectable({
  providedIn: 'root'
})
export class GlobalMessagesService {
  private hubConnection: signalR.HubConnection;
  
  // 🟢 State Management
  private unreadCountSubject = new BehaviorSubject<number>(0);
  public unreadCount$ = this.unreadCountSubject.asObservable();
  
  private latestMessagesSubject = new BehaviorSubject<ChatNotification[]>([]);
  public latestMessages$ = this.latestMessagesSubject.asObservable();

  constructor(private http: HttpClient, private authService: AuthService) {
    this.initializeSignalR();
    this.loadInitialUnreadCount();
  }

  // 1️⃣ Initialize SignalR (Notification Hub)
  private initializeSignalR() {
    const token = localStorage.getItem('token');
    if (!token) return;

    this.hubConnection = new signalR.HubConnectionBuilder()
      .withUrl(`${environment.apiUrl}/notificationHub`, {
        accessTokenFactory: () => token
      })
      .withAutomaticReconnect()
      .build();

    this.hubConnection.start()
      .then(() => console.log('🔔 Notification Hub Connected'))
      .catch(err => console.error('Notification Hub Error:', err));

    // 👂 Listen for Real-time Notifications
    this.hubConnection.on('ReceiveChatNotification', (data: ChatNotification) => {
      console.log('📨 New Notification Received:', data);

      // A. Update Badge Count
      if (data.unreadCount !== undefined) {
        this.unreadCountSubject.next(data.unreadCount);
      } else {
        this.unreadCountSubject.next(this.unreadCountSubject.value + 1);
      }

      // B. Add to Dropdown List
      const currentMsgs = this.latestMessagesSubject.value;
      this.latestMessagesSubject.next([data, ...currentMsgs].slice(0, 5)); // Keep last 5
    });
  }

  // 2️⃣ Load Initial State from API
  public loadInitialUnreadCount() {
    // You might need to update the endpoint if your backend uses a different path
    this.http.get<{ count: number }>(`${environment.apiUrl}/api/DesignRequest/chat/unread-count`)
      .subscribe({
        next: (res) => this.unreadCountSubject.next(res.count),
        error: (err) => console.error('Failed to load unread count', err)
      });
  }

  // 3️⃣ Actions
  public markAsRead(relatedId: string) {
    // Optimistic Update: Re-fetch count or decrement
    this.loadInitialUnreadCount();
  }
}
```

## 2. Update `NavbarComponent` (TS)
Inject the service and bind to the observable.

```typescript
import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { GlobalMessagesService, ChatNotification } from 'src/app/services/global-messages.service';

@Component({ ... })
export class NavbarComponent implements OnInit {
  unreadCount$ = this.messagesService.unreadCount$;
  latestMessages$ = this.messagesService.latestMessages$;
  isDropdownOpen = false;

  constructor(private messagesService: GlobalMessagesService, private router: Router) {}

  ngOnInit() {
    // Service auto-initializes logic
  }

  toggleDropdown() {
    this.isDropdownOpen = !this.isDropdownOpen;
  }

  onMessageClick(notification: ChatNotification) {
    this.isDropdownOpen = false;
    
    // Navigate to valid workspace
    if (notification.type === 'Chat') {
      // Assuming relatedId is RequestID for Designer Chat
      // If it's Property Chat, you might need detection logic
      this.router.navigate(['/project-workspace', notification.relatedId]);
    }
  }

  onViewAll() {
    this.isDropdownOpen = false;
    this.router.navigate(['/messages']);
  }
}
```

## 3. Update `NavbarComponent` (HTML)
Use standard Angular `async` pipe.

```html
<!-- Messages Icon -->
<div class="relative cursor-pointer mr-4" (click)="toggleDropdown()">
  <i class="pi pi-envelope text-2xl text-gray-600"></i>
  
  <!-- BADGE -->
  <span *ngIf="(unreadCount$ | async) as count" 
        [class.hidden]="count === 0"
        class="absolute -top-2 -right-2 bg-red-600 text-white text-xs font-bold rounded-full h-5 w-5 flex items-center justify-center">
    {{ count > 99 ? '99+' : count }}
  </span>

  <!-- Dropdown -->
  <div *ngIf="isDropdownOpen" class="absolute right-0 mt-3 w-80 bg-white shadow-xl rounded-lg z-50 border border-gray-100 overflow-hidden">
    <div class="p-3 bg-gray-50 border-b font-semibold text-gray-700">Notifications</div>
    
    <ul class="max-h-64 overflow-y-auto">
      <li *ngFor="let msg of latestMessages$ | async" 
          (click)="onMessageClick(msg)"
          class="p-3 border-b hover:bg-blue-50 cursor-pointer flex flex-col transition-colors">
         <span class="font-bold text-sm text-gray-800">{{ msg.title }}</span>
         <span class="text-xs text-gray-600 truncate mt-1">{{ msg.message }}</span>
         <span class="text-[10px] text-gray-400 text-right mt-1">{{ msg.sentDate | date:'shortTime' }}</span>
      </li>
      <li *ngIf="(latestMessages$ | async)?.length === 0" class="p-4 text-center text-gray-400 text-sm">
        No new messages
      </li>
    </ul>
    
    <div class="p-3 text-center text-blue-600 hover:text-blue-800 text-sm font-medium cursor-pointer bg-gray-50 hover:bg-gray-100 transition-colors" 
         (click)="onViewAll()">
      View All Messages
    </div>
  </div>
</div>
```
