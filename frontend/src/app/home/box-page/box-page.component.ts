import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import {State} from "../../../state";
import {Box, ResponseDto} from "../../models/box";
import { ModalController, ToastController } from '@ionic/angular'
import {environment} from "../../../environments/environment";
import { HttpClient } from '@angular/common/http';
import { firstValueFrom } from 'rxjs';


@Component({
  selector: 'app-box-page',
  templateUrl: './box-page.component.html',
  styleUrls: ['./box-page.component.scss'],
})
export class BoxPageComponent  implements OnInit {
  box: Box = null

  constructor(
    private route: ActivatedRoute,
    public state: State,
    public toastController: ToastController,
    public http: HttpClient,
    public router: Router
  )
  {

  }

  ngOnInit() {

    const boxId = this.route.snapshot.params['id'];
    this.box = this.state.boxes.find(value => value.boxId== boxId)

  }

  async updateBox() {
    // Check if all required fields are filled
    if (!this.box.boxName || !this.box.boxWeight) {
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
      const response = await firstValueFrom(this.http.put<ResponseDto<Box>>(environment.baseUrl + '/api/box/' + this.box.boxId, this.box)).then(async (res) => {
        if (res.messageToClient) {
          this.state.boxes.map(box => box.boxId !== res.responseData.boxId ? box : res.responseData);
          const toast = await this.toastController.create({
            message: res.messageToClient,
            duration: 2000,
            position: 'middle',
            color: 'success',
          });
          toast.present();
          this.router.navigate(["/home"])
        }
      })

        .catch(async (err) => {
          const toast = await this.toastController.create({
            message: err.message,
            duration: 2000,
            position: 'middle',
            color: 'danger',
          });
          toast.present();
          throw err;
        });

    } catch (error) {
      console.error('Error updating box:', error);

      const toast = await this.toastController.create({
        message: error.message,
        duration: 2000,
        position: 'middle',
        color: 'danger',
      });
      toast.present();
    }
  }
}
