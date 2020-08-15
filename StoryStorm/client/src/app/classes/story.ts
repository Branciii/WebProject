import { Genre } from './genre';

export class Story {
    StoryID : string;
    Author : string;
    Title : string;
    Grade : number;
    Description : string;
    Finished : number;
    Genres : Array<Genre>
}
