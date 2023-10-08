import { Component, OnInit } from '@angular/core';
import { firstValueFrom } from 'rxjs';
import { environment } from '../../environments/environment';
import { ModalController, ToastController } from '@ionic/angular';
import { HttpClient } from '@angular/common/http';
import { Box, ResponseDto } from '../models/box';
import { State } from '../../state';

@Component({
  selector: 'app-home',
  templateUrl: 'home.page.html',
  styleUrls: ['home.page.scss'],
})
export class HomePage implements OnInit {
  newBox: any = {
    boxName: '', // Remove boxId from the payload
    boxWeight: '',
  };

  constructor(
    public http: HttpClient,
    public modalController: ModalController,
    public state: State,
    public toastController: ToastController
  ) {}

  async fetchBoxes() {
    const result = await firstValueFrom(
      this.http.get<ResponseDto<Box[]>>(environment.baseUrl + '/api/box')
    );
    this.state.boxes = result.responseData || [];
    console.log(this.state.boxes);
  }

  ngOnInit(): void {
    this.fetchBoxes();
  }

  async createBox() {
    // Check if all required fields are filled
    if (!this.newBox.boxName || !this.newBox.boxWeight) {
      const toast = await this.toastController.create({
        message: 'Please fill in all required fields',
        duration: 2000,
        position: 'middle',
        color: 'danger',
      });
      toast.present();
      return;
    }

    // Send a POST request to create a new box
    try {
      const response = await this.http.post(environment.baseUrl + '/api/box', this.newBox).toPromise();
      const createdBox: Box = response as Box;

      // Add the newly created box to the state
      this.state.boxes.push(createdBox);

      // Clear the form
      this.newBox = {
        boxName: '',
        boxWeight: '',
      };

      const toast = await this.toastController.create({
        message: 'Box created successfully',
        duration: 2000,
        position: 'middle',
        color: 'success',
      });
      toast.present();
    } catch (error) {
      console.error('Error creating box:', error);

      const toast = await this.toastController.create({
        message: 'Failed to create box',
        duration: 2000,
        position: 'middle',
        color: 'danger',
      });
      toast.present();
    }
  }
}
