export class Token {
    static parse(token: string): any {
        let payload: any;
        let data: any;
        data = token.split('.')[1];
        data = window.atob(data);
        payload = JSON.parse(data);
        return payload;
    }

    static save(token: string): void {
        window.localStorage["ngcloud-token"] = token;
    } 

    static remove(): void {
        window.localStorage.removeItem("ngcloud-token");
    }

    static get(): string {
        return window.localStorage["ngcloud-token"];
    }
}