import { inject } from "@angular/core";
import { AuthService } from "../services/auth.service";
import { HttpInterceptorFn } from "@angular/common/http";


// the interceptor func is using for adding the token to the request header 
export const authInterceptor: HttpInterceptorFn = (req, next) =>  {
    const authService = inject(AuthService);
    const token = authService.getToken();

    if(token !== null){
        const cloneReq = req.clone({
            headers: req.headers.set("Authorization", `Bearer ${token}`) // add or change header then return the clone
        })
        return next(cloneReq); // request
    }
    return next(req);
}