import { Genre } from './genre';

export class Story {
    Author : string;
    Title : string;
    Grade : number;
    Description : string;
    Finished : number;
    Genre : Array<Genre>
}
