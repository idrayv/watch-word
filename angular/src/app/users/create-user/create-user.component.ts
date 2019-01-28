import { Component, ViewChild, Injector, Output, EventEmitter, ElementRef, OnInit } from '@angular/core';
import { ModalDirective } from 'ngx-bootstrap';
import { UserServiceProxy, CreateUserDto, RoleDto } from '@shared/service-proxies/service-proxies';
import { AppComponentBase } from '@shared/app-component-base';

@Component({
  selector: 'ww-create-user-modal',
  templateUrl: './create-user.component.html'
})
export class CreateUserComponent extends AppComponentBase implements OnInit {
  @ViewChild('createUserModal') modal: ModalDirective;
  @ViewChild('modalContent') modalContent: ElementRef;

  @Output() modalSave: EventEmitter<any> = new EventEmitter<any>();

  active = false;
  saving = false;
  user: CreateUserDto = null;
  roles: RoleDto[] = null;

  constructor(injector: Injector,
              private _userService: UserServiceProxy) {
    super(injector);
  }

  ngOnInit(): void {
    this._userService.getRoles()
      .subscribe((result) => {
        this.roles = result.items;
      });
  }

  show(): void {
    this.active = true;
    this.modal.show();
    this.user = new CreateUserDto();
    this.user.init({isActive: true});
  }

  onShown(): void {
    $.AdminBSB.input.activate($(this.modalContent.nativeElement));
  }

  save(): void {
    // TODO: Refactor this, don't use jQuery style code
    const roles = [];
    $(this.modalContent.nativeElement).find('[name=role]').each((ind: number, elem: Element) => {
      if ($(elem).is(':checked') === true) {
        roles.push(elem.getAttribute('value').valueOf());
      }
    });

    this.user.roleNames = roles;
    this.saving = true;
    this._userService.create(this.user)
      .finally(() => {
        this.saving = false;
      })
      .subscribe(() => {
        this.notify.info(this.l('SavedSuccessfully'));
        this.close();
        this.modalSave.emit(null);
      });
  }

  close(): void {
    this.active = false;
    this.modal.hide();
  }
}
