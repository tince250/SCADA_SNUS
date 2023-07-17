import { AuthService } from './auth.service';
import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { environment } from '../../environments/environment';
import { map, Subject } from 'rxjs';
import SockJS from 'sockjs-client';
import Stomp, { Message } from 'stompjs';
// import sockjs from "sockjs-client/dist/sockjs"
import { MatDialog } from '@angular/material/dialog';
import { HttpTransportType, HubConnectionBuilder, LogLevel } from '@microsoft/signalr';

@Injectable({
  providedIn: 'root',
})
export class InputTagValueSocketService {
    url: string = environment.apiHost + "/ws"
    ws: any;
    stompClient: any;
    isConnected: boolean = false;
    isConnectedRes: boolean = false;

    private inputTagValue = new Subject<string>();
    inputTagValueSub: any;

    constructor() { }

    subscribeToInputTagValueUpdated() {
        this.inputTagValueSub = this.stompClient.subscribe("/updateInput", (message: Message) =>  {
            this.updateInputTagValue(message.body);
        });   
    }

    unsubscribeFromInputTagValue() {
        if (this.inputTagValueSub != undefined)
            this.inputTagValueSub.unsubscribe();
    }

    receivedInputTagValue() {
        return this.inputTagValue.asObservable();
    }

    updateInputTagValue(res: string) {
        this.inputTagValue.next(res);
    }

    openWebSocketConnection() {
        if (this.isConnected) {
            return;
        }
        this.ws = new SockJS(this.url);
        this.stompClient = Stomp.over(this.ws);
        this.stompClient.connect({}, () => {
            this.isConnected = true;
            this.subscribeToInputTagValueUpdated();
        });
    }

    closeWebSocketConnection() {
        if (this.isConnected) {
            this.stompClient.disconnect();
            this.isConnected = false;
        }
    }

    socket: any;

    public connect(): void {
      const url = 'ws://localhost:7012/ws'; // Replace with your backend WebSocket URL
      this.socket = new WebSocket(url);
  
      this.socket.onopen = (event: any) => {
        console.log('WebSocket connection opened.');
      };
  
      this.socket.onmessage = (event:any) => {
        const data = JSON.parse(event.data);
        console.log('Received WebSocket data:', data);
        // Handle the data received from the WebSocket server here
      };
  
      this.socket.onclose = (event:any) => {
        console.log('WebSocket connection closed.');
      };
  
      this.socket.onerror = (event:any) => {
        console.error('WebSocket error:', event);
      };
    }
  
    public close(): void {
      if (this.socket) {
        this.socket.close();
      }
    }
  
    // Function to send data to the WebSocket server
    public sendData(data: any): void {
      if (this.socket && this.socket.readyState === WebSocket.OPEN) {
        this.socket.send(JSON.stringify(data));
      }
    }
    connection: any;
    messages: any;

    initWebSocket() {
      this.connection = new HubConnectionBuilder()
        .configureLogging(LogLevel.Debug)
        .withUrl('https://localhost:7012/hub/updateInput', {
          skipNegotiation: true,
          transport: HttpTransportType.WebSockets
        })
        .build();
      this.connection
        .start()
        .then(() => console.log('Connection started'))
        .catch(() => console.log('Error while starting connection: '))
      this.connection.on('input', (from: string, body: string) => {
        console.log("uspesno");
      });
    }

  
}