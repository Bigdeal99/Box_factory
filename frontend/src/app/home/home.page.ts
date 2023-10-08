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
    // ... your existing createBox code ...

    try {
      // ... your existing createBox code ...

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

  async deleteBox(box: Box) {
    try {
      // Send a DELETE request to delete the box
      await this.http
        .delete(environment.baseUrl + '/api/box/' + box.boxId)
        .toPromise();

      // Remove the deleted box from the state
      const index = this.state.boxes.findIndex((b) => b.boxId === box.boxId);
      if (index !== -1) {
        this.state.boxes.splice(index, 1);
      }

      const toast = await this.toastController.create({
        message: 'Box deleted successfully',
        duration: 2000,
        position: 'middle',
        color: 'success',
      });
      toast.present();
    } catch (error) {
      console.error('Error deleting box:', error);

      const toast = await this.toastController.create({
        message: 'Failed to delete box',
        duration: 2000,
        position: 'middle',
        color: 'danger',
      });
      toast.present();
    }
  }
}
