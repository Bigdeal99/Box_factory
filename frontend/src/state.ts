import { Injectable } from "@angular/core";
import {Box} from "./app/models/box";


@Injectable({
  providedIn: 'root'
})

export class State {
  boxes: Box  [] = [] || undefined;
}
