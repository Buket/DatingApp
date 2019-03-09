import { Component, OnInit } from '@angular/core';
import { Photo } from 'src/app/_models/photo';
import { AdminService } from 'src/app/_services/admin.service';
import { AlertifyService } from 'src/app/_services/alertify.service';

@Component({
  selector: 'app-photo-management',
  templateUrl: './photo-management.component.html',
  styleUrls: ['./photo-management.component.css']
})
export class PhotoManagementComponent implements OnInit {
  photos: Photo[];
  constructor(private adminService: AdminService,
    private alertify: AlertifyService) { }

  ngOnInit() {
    // load not approved photos
    this.adminService.getPhotoForModeration().subscribe((photos: Photo[]) => {
      this.photos = photos;
    }, error => {
      console.log(error);
    });
  }

  approve(id: number) {
    // send approve photo request
    this.adminService.approvePhoto(id).subscribe(() => {
      this.photos.splice(this.photos.findIndex(p => p.id === id), 1);
    }, error => {
      this.alertify.error(error);
    });
  }

  deny(id: number) {
    this.adminService.denyPhoto(id).subscribe(() => {
      this.photos.splice(this.photos.findIndex(p => p.id === id), 1);
    }, error => {
      this.alertify.error(error);
    });
  }

}
